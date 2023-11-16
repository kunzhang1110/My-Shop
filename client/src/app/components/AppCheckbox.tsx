import { Checkbox, FormControlLabel } from "@mui/material";
import { useController, UseControllerProps } from "react-hook-form";

interface Props extends UseControllerProps {
  label: string;
  disabled: boolean;
}

export default function AppCheckbox(props: Props) {
  const { field } = useController({
    control: props.control,
    name: props.name,
  });

  return (
    <FormControlLabel
      control={
        <Checkbox
          {...field}
          checked={field.value}
          color="secondary"
          disabled={props.disabled}
        />
      }
      label={props.label}
    />
  );
}
