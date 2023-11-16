import * as yup from "yup";

export const validationSchema = yup.object({
  name: yup.string().required(),
  brand: yup.string().required(),
  type: yup.string().required(),
  price: yup.number().required().moreThan(1),
  quantityInStock: yup.number().required().min(0),
  description: yup.string().required(),
  image: yup.mixed().when("pictureUrl", {
    is: (value: string) => {
      return !value;
    }, //when image.productUrl is undefined
    then: (schema) => schema.required("Please provide an image"),
    otherwise: (schema) => schema.notRequired(),
  }),
});
