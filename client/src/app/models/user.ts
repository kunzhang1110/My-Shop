import { Basket } from "./basket";

export interface User {
  userName: string;
  email: string;
  token: string;
  basket?: Basket;
  roles?: string[];
}
