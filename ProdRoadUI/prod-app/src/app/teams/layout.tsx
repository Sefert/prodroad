import ItemCards from "@/components/listBar/ItemCards"

//https://stackoverflow.com/questions/49007357/how-to-make-the-whole-card-component-clickable-in-material-ui-using-react-js
export default function TeamsId({
    children,
  }: Readonly<{
    children: React.ReactNode;
  }>){
    return (
       <ItemCards>
            {children}
       </ItemCards>
    )
}