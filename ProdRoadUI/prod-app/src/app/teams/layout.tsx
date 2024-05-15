import ItemCards from "@/components/listBar/ItemCards"

//https://stackoverflow.com/questions/49007357/how-to-make-the-whole-card-component-clickable-in-material-ui-using-react-js
export default function Teams({
    children,
  }: Readonly<{
    children: React.ReactNode;
  }>){
    return (
       <ItemCards<string> 
          elemList= {['TEAM1','TEAM2']} 
          marginLeft={0}
          itemCardProp={{
            addMain:"NEW TEAM",
            addMainPath: '/teams/persons/',
            addOther: "CONNECT PERSON",
            addOtherPath: '/teams/person/',
            showAddMain: true,
            showAddOther: true,
        }}
        >
            {children}
       </ItemCards>
    )
}