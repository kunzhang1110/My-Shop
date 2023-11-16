import {
  Container,
  CssBaseline,
  ThemeProvider,
  createTheme,
} from "@mui/material";
import Header from "./Header";
import { Outlet, useLocation } from "react-router-dom";
import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import { useCallback, useEffect, useState } from "react";
import LoadingComponent from "./LoadingComponent";
import { fetchBasketItemAsync } from "../../features/basket/basketSlice";
import { fetchCurrentUser } from "../../features/account/accountSlice";
import { useAppDispatch } from "../store/configureStore";
import HomePage from "../../features/home/HomePage";

export default function App() {
  const [loading, setLoading] = useState(true);
  const location = useLocation();
  const dispatch = useAppDispatch();

  const initApp = useCallback(async () => {
    try {
      await dispatch(fetchCurrentUser());
      await dispatch(fetchBasketItemAsync());
    } catch (error) {
      console.log(error);
    }
  }, [dispatch]);

  useEffect(() => {
    initApp().then(() => setLoading(false));
  }, [initApp]);

  const theme = createTheme({
    palette: {
      mode: "light",
      background: {
        default: "#f5f5f5",
      },
    },
  });

  return (
    <ThemeProvider theme={theme}>
      <CssBaseline />
      <Header />
      {loading ? (
        <LoadingComponent message="Initialising app..." />
      ) : location.pathname === "/" ? (
        <HomePage />
      ) : (
        <Container sx={{ mt: 4 }}>
          <Outlet />
        </Container>
      )}
      <ToastContainer position="bottom-right" hideProgressBar theme="colored" />
    </ThemeProvider>
  );
}
