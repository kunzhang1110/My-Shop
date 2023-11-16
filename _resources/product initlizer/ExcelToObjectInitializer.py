import pandas as pd


df = pd.read_excel("./products.xlsx", index_col=None)
class_name = "Product"


output_string = ""
for index, row in df.iterrows():
    output_string += f'new {class_name}\n{{\n'
    for index, value in row.items():
        if (isinstance(value, str)):
            value = value.replace('"', '\\"')
            output_string += f'\t{index} = "{value}"'
        else:
            output_string += f'\t{index} =  {value}'
        # if it is the last property, no "\n" needed
        if (index != df.columns[df.columns.size-1]):
            output_string += ',\n'
    output_string += "\n},\n"
# print(output_string)

f = open("output.txt", "w")
f.write(output_string)
f.close()
