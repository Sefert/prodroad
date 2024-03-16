using App.Contracts.DAL;

namespace App.DAL.EF;

public class DalMapper<TLeftObject, TRightObject> : IDalMapper<TLeftObject,TRightObject>
    where TLeftObject : class 
    where TRightObject : class
{
    public TLeftObject? MapRL(TRightObject? inObject)
    {
        return inObject as TLeftObject;
    }

    public TRightObject? MapLR(TLeftObject? inObject)
    {
        return inObject as TRightObject;
    }
}
