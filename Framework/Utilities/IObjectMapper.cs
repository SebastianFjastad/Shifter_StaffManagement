namespace Framework.Utilities
{
    /// <summary>
    ///Defines a typed  assembler "mapping" contract from one type to another.
    ///<para>See http://www.martinfowler.com/eaaCatalog/dataTransferObject.html for more information regarding the assembler pattern.</para>
    /// </summary>
    //public interface IObjectMapper<in TIn, out TOut>
    public interface IObjectMapper<in TIn, TOut>
    {
        /// <summary>
        /// Defines the signature of an operation which maps two objects.
        /// </summary>
        TOut MapObjects(TIn inObject);

        /// <summary>
        /// Defines the signature of an operation which maps an object to an existing object.
        /// </summary>
        TOut MapObjects(TIn inObject, TOut refObject);
    }
}