import {
  Box,
  List,
  ListItem,
  ListItemIcon,
  ListItemText,
  Typography,
} from "@mui/material";
import { Business, Email, LocalPhone } from "@mui/icons-material";

export default function ContactPage() {
  return (
    <>
      <Typography variant="h2">Contact Us</Typography>
      <Box sx={{ m: 2 }}>
        <List>
          <ListItem>
            <ListItemIcon>
              <Email />
            </ListItemIcon>
            <ListItemText
              primary="Email: kunzhang1110@gmail.com"
              primaryTypographyProps={{ variant: "h6" }}
            />
          </ListItem>
          <ListItem>
            <ListItemIcon>
              <LocalPhone />
            </ListItemIcon>
            <ListItemText
              primary="6149039283"
              primaryTypographyProps={{ variant: "h6" }}
            />
          </ListItem>
          <ListItem>
            <ListItemIcon>
              <Business />
            </ListItemIcon>
            <ListItemText
              primary="123 Main Street Sydney, NSW 2000 Australia"
              primaryTypographyProps={{ variant: "h6" }}
            />
          </ListItem>
        </List>
      </Box>
    </>
  );
}
