import { TextField } from "@mui/material";
import { useController, UseControllerProps } from "react-hook-form";

interface Props extends UseControllerProps {
  label: string;
  multiline?: boolean;
  rows?: number;
  type?: string;
  InputLabelProps?: object;
}

export default function AppTextInput(props: Props) {
  const { field, fieldState } = useController({
    name: props.name,
    control: props.control,
    defaultValue: "",
  });

  return (
    <TextField
      onChange={field.onChange} // send value to hook form
      onBlur={field.onBlur} // notify when input is touched/blur
      value={field.value} // input value
      name={field.name} // send down the input name
      inputRef={field.ref} // send input ref, so we can focus on input when error appear
      multiline={props.multiline}
      label={props.label}
      rows={props.rows}
      type={props.type}
      fullWidth
      variant="outlined"
      error={!!fieldState.error}
      helperText={fieldState.error?.message}
      InputLabelProps={props.InputLabelProps}
    />
  );
}
