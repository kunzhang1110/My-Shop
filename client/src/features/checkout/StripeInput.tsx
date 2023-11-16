import { InputBaseComponentProps } from "@mui/material";
import { forwardRef, Ref, useImperativeHandle, useRef } from "react";

//See https://mui.com/material-ui/react-text-field/#integration-with-3rd-party-input-libraries

interface Props extends InputBaseComponentProps {}

export const StripeInput = forwardRef(function StripeInput(
  { component: Element, ...props }: Props,
  ref: Ref<any>
) {
  const elementRef = useRef<any>();

  useImperativeHandle(ref, () => {
    return {
      //expose only Element's focus() method; so that ref to StriptInput can call Element's focus() method. This is required by MUI Textfield
      focus: () => elementRef.current.focus,
    };
  });

  return (
    // When Stripe Element is fully rendered, assigned the Element to elementRef.current
    <Element
      onReady={(element: any) => (elementRef.current = element)}
      {...props}
    />
  );
});
