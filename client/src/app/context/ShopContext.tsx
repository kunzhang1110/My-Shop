import { createContext, PropsWithChildren, useContext, useState } from "react";
import { Basket } from "../models/basket";

interface ShopContextValue {
  removeItem: (productId: number, quantity: number) => void;
  setBasket: (basket: Basket) => void;
  basket: Basket | null;
}

export const ShopContext = createContext<ShopContextValue | undefined>(
  undefined
);

export function useShopContext() {
  let context = useContext(ShopContext);
  if (context === undefined) {
    throw Error("Not inside a provider");
  }
  return context;
}

export function ShopProvider({ children }: PropsWithChildren<any>) {
  const [basket, setBasket] = useState<Basket | null>(null);

  function removeItem(productId: number, quantity: number) {
    if (!basket) return;
    const items = [...basket.items]; // new array of items
    const itemIndex = items.findIndex((i) => i.productId === productId);
    if (itemIndex >= 0) {
      items[itemIndex].quantity -= quantity;
      if (items[itemIndex].quantity === 0) items.splice(itemIndex, 1);
      setBasket((prevState) => {
        return { ...prevState!, items };
      });
    }
  }

  return (
    <ShopContext.Provider value={{ basket, setBasket, removeItem }}>
      {children}
    </ShopContext.Provider>
  );
}
