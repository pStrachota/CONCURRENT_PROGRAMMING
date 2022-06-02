using Serilog;
using Serilog.Formatting.Compact;
using System.Data;

namespace DATA_LAYER;

public abstract class DataLayerAbstractApi
{
    public int BOX_WIDTH = 1445;
    public int BOX_HEIGHT = 504;

    private static ILogger _logger;

    public abstract ILogger GetLogger();
    public abstract List<IDLCircle> GetDllCirclesFromBox(int numberOfBalls, int minRadius, int maxRadius, int speed);
    public static DataLayerAbstractApi? CreateLinq2DLCircles()
    {
        _logger = new LoggerConfiguration()
               .WriteTo.Async(a => a.Debug())
               .WriteTo.Async(a => a.MSSqlServer(
                   connectionString: "Data Source=DESKTOP-NAMFMUD;Initial Catalog=CircleDiagnosticData; Integrated Security=True",
                   sinkOptions: new Serilog.Sinks.MSSqlServer.MSSqlServerSinkOptions
                   {
                       TableName = "Logs",
                       AutoCreateSqlTable = true
                   }))
               .WriteTo.Async(a => a.File(
                   new RenderedCompactJsonFormatter(), @"C:\Users\lenovo\Desktop\LOGI\logJson.json"))
               .WriteTo.Async(a => a.File(
                   @"C:\Users\lenovo\Desktop\LOGI\logTxt.txt"))
               .CreateLogger();

        return new Linq2DLCircles();
    }

    private class Linq2DLCircles : DataLayerAbstractApi
    {
        public override List<IDLCircle> GetDllCirclesFromBox(int numberOfBalls, int minRadius, int maxRadius, int speed)
        {
            return new Box(BOX_WIDTH, BOX_HEIGHT, numberOfBalls, minRadius, maxRadius, speed).GetDllCircles();
        }

        public override ILogger GetLogger()
        {
            return _logger;
        }
    }
}