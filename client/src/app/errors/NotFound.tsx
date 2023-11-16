import { Button, Container, Divider, Paper, Typography } from "@mui/material";
import { Link } from "react-router-dom";

export default function NotFound() {
  return (
    <Container component={Paper} sx={{ height: 400, textAlign: "center" }}>
      <Typography gutterBottom variant="h3">
        Page not found.
      </Typography>
      <Divider />
      <Button fullWidth component={Link} to="/catalog">
        Go back to catalog
      </Button>
    </Container>
  );
}
