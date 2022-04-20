using System.Data;

namespace DATA_LAYER;

public abstract class DataLayerAbstractApi
{
    public static string ConString = "SAMPLE_CONNECTION_STRING";
    
    public abstract DataTable? ReadCircleDetails();
    public static DataLayerAbstractApi CreateLinq2Sql()
    {
        return new Linq2Sql();
    }

    private class Linq2Sql : DataLayerAbstractApi
    {
        public override DataTable? ReadCircleDetails()
        {
            return null;
        }
    }
}