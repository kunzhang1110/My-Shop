import {
  Avatar,
  Button,
  Card,
  CardActions,
  CardContent,
  CardHeader,
  CardMedia,
  Typography,
} from "@mui/material";
import { Product } from "../../app/models/product";
import { Link } from "react-router-dom";
import { LoadingButton } from "@mui/lab";
import { useAppDispatch, useAppSelector } from "../../app/store/configureStore";
import { addBasketItemAsync } from "../basket/basketSlice";

interface Props {
  product: Product;
}

export default function ProductCard({ product }: Props) {
  const dispatch = useAppDispatch();
  const { status } = useAppSelector((state) => state.basket);

  return (
    <Card>
      <CardHeader
        avatar={
          <Avatar sx={{ bgcolor: "primary.light", color: "#ffffff" }}>
            {product.name.charAt(0).toUpperCase()}
          </Avatar>
        }
        title={
          product.name.length > 50
            ? product.name.substring(0, 48) + "..."
            : product.name
        }
        titleTypographyProps={{
          sx: { fontWeight: "bold" },
        }}
      />
      <CardMedia
        sx={{
          height: 160,
          backgroundSize: "contain",
        }}
        image={product.pictureUrl}
        title={product.name}
      />
      <CardContent>
        <Typography gutterBottom color="primary.main" variant="h5">
          ${(product.price / 100).toFixed(2)}
        </Typography>
        <Typography variant="body2" color="text.secondary">
          {product.brand}/{product.type}
        </Typography>
      </CardContent>
      <CardActions>
        <LoadingButton
          loading={status === `pendingAddItem ${product.id}`}
          onClick={() =>
            dispatch(addBasketItemAsync({ productId: product.id, quantity: 1 }))
          }
          size="small"
        >
          Add to Cart
        </LoadingButton>
        <Button component={Link} to={`/catalog/${product.id}`} size="small">
          View
        </Button>
      </CardActions>
    </Card>
  );
}
