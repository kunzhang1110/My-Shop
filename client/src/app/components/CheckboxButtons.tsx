import { FormGroup, FormControlLabel, Checkbox } from "@mui/material";
import { useState } from "react";

interface Props {
  items: string[];
  checked?: string[];
  onChange: (items: string[]) => void;
}

export default function CheckboxButtons({ items, checked, onChange }: Props) {
  const [checkedItems, setCheckedItems] = useState(checked || []);

  function handleChecked(value: string) {
    let newCheckedItems: string[] = [];
    const currentIndex = checkedItems.findIndex((item) => item === value);
    if (currentIndex === -1) newCheckedItems = [...checkedItems, value];
    else newCheckedItems = checkedItems.filter((i) => i !== value); //remove check
    setCheckedItems(newCheckedItems);
    onChange(newCheckedItems);
  }

  return (
    <FormGroup>
      {items.map((item) => (
        <FormControlLabel
          key={item}
          control={
            <Checkbox
              checked={checkedItems.indexOf(item) !== -1}
              onClick={() => handleChecked(item)}
            />
          }
          label={item}
        />
      ))}
    </FormGroup>
  );
}
