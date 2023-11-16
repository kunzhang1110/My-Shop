import { Elements } from "@stripe/react-stripe-js";
import CheckoutPage from "./CheckoutPage";
import { loadStripe } from "@stripe/stripe-js";
import { useState, useEffect } from "react";
import agent from "../../app/api/agent";
import { useAppDispatch } from "../../app/store/configureStore";
import { setBasket } from "../basket/basketSlice";
import LoadingComponent from "../../app/layout/LoadingComponent";

const stripePromise = loadStripe(
  "pk_test_51NG9IiDPpkprq16HS0zCBv0ZwAEo9jblhtOpSwer46roxdGbTYn6Ca44WpHf3e9xysW7AAiljQBIK8bVZaTNuxb000x2ognBxC"
);

export default function CheckoutWrapper() {
  const dispatch = useAppDispatch();
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    //get payment intent once in Checkout Page
    agent.Payments.createPaymentIntent()
      .then((response) => dispatch(setBasket(response)))
      .catch((error) => console.log(error))
      .finally(() => setLoading(false));
    setLoading(false);
  }, [dispatch]);

  if (loading) return <LoadingComponent message="Loading checkout" />;

  return (
    <Elements stripe={stripePromise}>
      <CheckoutPage />
    </Elements>
  );
}
