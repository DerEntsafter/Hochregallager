namespace Testhochhregal
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {

        }

        public enum PacketSize
        {
            Groesse1, 
            Groesse2,
            Groesse3,

        }

       
        public static class PacketChecker
        {
            public static PacketSize Parse(string blubb) => blubb switch
            {
                "small" => PacketSize.Groesse1,
                "medium" => PacketSize.Groesse2,
                "large" => PacketSize.Groesse3,
                var other => throw new ArgumentException($"\"{other}\" ist keine gültige Paketgröße", nameof(blubb)),
            };
        }
    }
}