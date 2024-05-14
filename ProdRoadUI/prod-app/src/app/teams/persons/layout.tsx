import ItemCards from "@/components/listBar/ItemCards"

//https://stackoverflow.com/questions/49007357/how-to-make-the-whole-card-component-clickable-in-material-ui-using-react-js
export default function Personslist({
    children,
  }: Readonly<{
    children: React.ReactNode;
  }>){
    return (
       <ItemCards<string> elemList= {['TEAM1','TEAM2']}>
            {children}
       </ItemCards>
    )
}