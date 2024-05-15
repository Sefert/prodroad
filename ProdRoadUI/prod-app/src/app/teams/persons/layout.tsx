import ItemCards from "@/components/listBar/ItemCards"
import { devNull } from "os";

//https://stackoverflow.com/questions/49007357/how-to-make-the-whole-card-component-clickable-in-material-ui-using-react-js
export default function Personslist({
    children,
  }: Readonly<{
    children: React.ReactNode;
  }>){
    return (
       <ItemCards<string> 
            elemList= {['PERSON1','PERSON2']} 
            marginLeft={0}
            itemCardProp={{
                addMain: undefined,
                addMainPath: undefined,
                addOther: undefined,
                addOtherPath: undefined,
                showAddMain: false,
                showAddOther: false,
            }}
        >
            {children}
       </ItemCards>
    )
}