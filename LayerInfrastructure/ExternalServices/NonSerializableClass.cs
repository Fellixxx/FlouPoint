namespace LayerInfrastructure.ExternalServices
{
    public class NonSerializableClass
    {
        public string SomeProperty { get; set; }

        [NonSerialized]
        public object NonSerializableField;

        public NonSerializableClass()
        {
            SomeProperty = "SomeValue";
            NonSerializableField = new object();
        }
    }

}
