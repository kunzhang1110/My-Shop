import {
  createAsyncThunk,
  createEntityAdapter,
  createSlice,
  isAnyOf,
} from "@reduxjs/toolkit";
import { Product, ProductParams } from "../../app/models/product";
import agent from "../../app/api/agent";
import { RootState } from "../../app/store/configureStore";
import { MetaData } from "../../app/models/pagination";

interface CatalogState {
  productsLoaded: boolean;
  filtersLoaded: boolean;
  status: string;
  brands: string[];
  types: string[];
  productParams: ProductParams;
  metaData: MetaData | null;
}

const productsAdapter = createEntityAdapter<Product>();

export const fetchProductAsync = createAsyncThunk<Product, number>(
  "catalog/fetchProductAsync",
  async (productId, thunkAPI) => {
    try {
      return await agent.Catalog.details(productId);
    } catch (error: any) {
      return thunkAPI.rejectWithValue({ error: error.data });
    }
  }
);

function getAxiosParams(productParams: ProductParams) {
  const params = new URLSearchParams();
  params.append("pageNumber", productParams.pageNumber.toString());
  params.append("pageSize", productParams.pageSize.toString());
  params.append("orderBy", productParams.orderBy);
  if (productParams.searchTerm)
    params.append("searchTerm", productParams.searchTerm);
  if (productParams.brands.length > 0)
    params.append("brands", productParams.brands.toString()); //array toString() adds "," between elements
  if (productParams.types.length > 0)
    params.append("types", productParams.types.toString());
  return params;
}

export const fetchProductsAsync = createAsyncThunk<
  Product[],
  void,
  { state: RootState }
>("catalog/fetchProductsAsync", async (_, thunkAPI) => {
  const params = getAxiosParams(thunkAPI.getState().catalog.productParams);
  try {
    const response = await agent.Catalog.list(params);
    thunkAPI.dispatch(setMetaData(response.metaData));
    return response.items;
  } catch (error: any) {
    thunkAPI.rejectWithValue({ error: error.data });
  }
});

export const fetchFiltersAsync = createAsyncThunk(
  "catalog/FetchFiltersAsync",
  async (_, thunkAPI) => {
    try {
      return await agent.Catalog.fetchFilters();
    } catch (error: any) {
      return thunkAPI.rejectWithValue({ error: error.data });
    }
  }
);

function initProductPrams() {
  return {
    pageNumber: 1,
    pageSize: 6,
    orderBy: "name",
    brands: [],
    types: [],
  };
}

export const catalogSlice = createSlice({
  name: "catalog",
  initialState: productsAdapter.getInitialState<CatalogState>({
    //more initial states other than "products"
    productsLoaded: false,
    filtersLoaded: false,
    status: "idle",
    brands: [],
    types: [],
    productParams: initProductPrams(),
    metaData: null,
  }),
  reducers: {
    setMetaData: (state, action) => {
      state.metaData = action.payload;
    },
    setPageNumber: (state, action) => {
      state.productsLoaded = false; // trigger fetchProductsAsync())in CatalogPage via useProduct()
      state.productParams = {
        ...state.productParams,
        ...action.payload,
      };
    },
    setProductsParams: (state, action) => {
      state.productsLoaded = false; // trigger fetchProductsAsync())in CatalogPage via useProduct()
      state.productParams = {
        ...state.productParams,
        ...action.payload,
        pageNumber: 1,
      };
    },
    resetProductsParams: (state) => {
      state.productParams = initProductPrams();
    },
    setProduct: (state, action) => {
      productsAdapter.upsertOne(state, action.payload);
      state.productsLoaded = false;
    },
    removeProduct: (state, action) => {
      productsAdapter.removeOne(state, action.payload);
      state.productsLoaded = false;
    },
  },
  extraReducers: (builder) => {
    builder.addCase(fetchProductAsync.pending, (state, action) => {
      state.status = "pendingFetchProduct";
    });
    builder.addCase(fetchProductAsync.fulfilled, (state, action) => {
      productsAdapter.upsertOne(state, action.payload);
      state.status = "idle";
    });

    builder.addCase(fetchProductsAsync.pending, (state, action) => {
      state.status = "pendingFetchProducts";
    });
    builder.addCase(fetchProductsAsync.fulfilled, (state, action) => {
      productsAdapter.setAll(state, action.payload);
      state.status = "idle";
      state.productsLoaded = true;
    });

    builder.addCase(fetchFiltersAsync.pending, (state) => {
      state.status = "pendingFetchFilters";
    });
    builder.addCase(fetchFiltersAsync.fulfilled, (state, action) => {
      state.brands = action.payload.brands;
      state.types = action.payload.types;
      state.filtersLoaded = true;
    });

    builder.addMatcher(
      isAnyOf(
        fetchProductAsync.rejected,
        fetchProductsAsync.rejected,
        fetchFiltersAsync.rejected
      ),
      (state, action) => {
        console.log(action);
        state.status = "idle";
      }
    );
  },
});

export const productSelectors = productsAdapter.getSelectors(
  (state: RootState) => state.catalog
);

export const {
  setProductsParams,
  setMetaData,
  setPageNumber,
  setProduct,
  removeProduct,
  resetProductsParams,
} = catalogSlice.actions;
