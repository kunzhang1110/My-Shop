import { Navigate, createBrowserRouter } from "react-router-dom";
import HomePage from "../../features/home/HomePage";
import App from "../layout/App";
import CatalogPage from "../../features/catalog/CatalogPage";
import ProductDetails from "../../features/catalog/ProductDetails";
import TestPage from "../../features/test/TestPage";
import ContactPage from "../../features/contact/ContactPage";
import ServerError from "../errors/ServerError";
import NotFound from "../errors/NotFound";
import BasketPage from "../../features/basket/BasketPage";
import Login from "../../features/account/Login";
import Register from "../../features/account/Register";
import RequireAuth from "./RequireAuth";
import Orders from "../../features/orders/Orders";
import CheckoutWrapper from "../../features/checkout/CheckoutWrapper";
import Inventory from "../../features/admin/Inventory";

export const router = createBrowserRouter([
  {
    path: "/",
    element: <App />,
    children: [
      {
        //authenticated routes
        element: <RequireAuth />,
        children: [
          { path: "checkout", element: <CheckoutWrapper /> },
          { path: "orders", element: <Orders /> },
          { path: "inventory", element: <Inventory /> },
        ],
      },
      {
        //admin routes
        element: <RequireAuth roles={["Admin"]} />,
        children: [{ path: "inventory", element: <Inventory /> }],
      },
      { path: "", element: <HomePage /> },
      { path: "catalog", element: <CatalogPage /> },
      { path: "catalog/:id", element: <ProductDetails /> },
      { path: "contact", element: <ContactPage /> },
      { path: "test", element: <TestPage /> },
      { path: "server-error", element: <ServerError /> },
      { path: "not-found", element: <NotFound /> },
      { path: "basket", element: <BasketPage /> },
      { path: "login", element: <Login /> },
      { path: "register", element: <Register /> },
      { path: "*", element: <Navigate replace to="/not-found" /> }, //other routes
    ],
  },
]);
