
namespace ExampleProjectWithWarnings
{
    // from http://msdn.microsoft.com/en-us/library/4bw5ewxy.aspx
    // produces CS0420
    public class TestClass
    {
        private volatile int i;

        public void TestVolatile(ref int ii)
        {
        }

        public static void Main()
        {
            TestClass x = new TestClass();
            x.TestVolatile(ref x.i);   // CS0420 
        }

    }
}
