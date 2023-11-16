import { Container, Divider, Paper, Typography } from "@mui/material";
import { useLocation } from "react-router-dom";

export default function ServerError() {
  const { state } = useLocation();

  return (
    <Container component={Paper}>
      {state?.error ? (
        <>
          <Typography variant="h3" color="secondary">
            {state.error.title}
          </Typography>
          <Divider />
          <Typography variant="body1" color="secondary">
            {state.error.detail || "Internal server error"}
          </Typography>
        </>
      ) : (
        <></>
      )}
    </Container>
  );
}
