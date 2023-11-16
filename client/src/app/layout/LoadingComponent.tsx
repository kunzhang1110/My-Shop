import { Backdrop, Box, CircularProgress, Typography } from "@mui/material";

interface Props {
  message?: string;
}

export default function LoadingComponent({ message = "Loading..." }: Props) {
  return (
    <Backdrop open={true} invisible={true}>
      <Box
        display="flex"
        flexDirection="column"
        justifyContent="center"
        alignItems="center"
        height="100vh"
      >
        <CircularProgress size={80} color="secondary" />
        <Typography
          variant="h4"
          sx={{
            mt: 4,
          }}
        >
          {message}
        </Typography>
      </Box>
    </Backdrop>
  );
}
