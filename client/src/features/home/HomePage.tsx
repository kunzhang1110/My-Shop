import { Box, Typography } from "@mui/material";
import Slider from "react-slick";

export default function HomePage() {
  const settings = {
    dots: true,
    infinite: true,
    speed: 500,
    slidesToShow: 1,
    slidesToScroll: 1,
  };

  return (
    <>
      <Slider {...settings}>
        <div>
          <img
            src="/images/cover1.jpg"
            alt="cover1"
            style={{ display: "block", width: "100%", maxHeight: 600 }}
          />
        </div>
        <div>
          <img
            src="/images/cover2.jpg"
            alt="cover2"
            style={{ display: "block", width: "100%", maxHeight: 600 }}
          />
        </div>
        <div>
          <img
            src="/images/cover3.jpg"
            alt="cover3"
            style={{ display: "block", width: "100%", maxHeight: 600 }}
          />
        </div>
      </Slider>
      <Box display="flex" justifyContent="center" sx={{ p: 4 }}>
        <Typography variant="h1">Welcome to Kun's Shop!</Typography>
      </Box>
    </>
  );
}
