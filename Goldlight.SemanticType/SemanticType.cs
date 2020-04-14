using System;
using System.Runtime.Serialization;

namespace Goldlight.SemanticTypes
{
    [Serializable]
    public class SemanticType<TK, T> : IEquatable<SemanticType<TK, T>>, IComparable<SemanticType<TK, T>>, ISerializable
        where T : IComparable<T>
        where TK : SemanticType<TK, T>,  new()
    {
        private Func<T, bool> _validator;
        private string _validationText;

        protected SemanticType()
        {
        }

        protected SemanticType(SerializationInfo info, StreamingContext context)
        {
            if (info == null) throw new ArgumentNullException(nameof(info));
            Value = (T) info.GetValue("Value", typeof(T));
        }

        public static TK TryCreate(T instance)
        {
            if (instance == null) throw new ArgumentException("You should not pass in a null value to TryCreate");
            TK type = new TK();
            type.SetValidation(t => true, "The validation failed");
            if (type._validator != null && !type._validator(instance))
                throw new ApplicationException(type._validationText);
            type.Value = instance;
            return type;
        }
            
        protected virtual void SetValidation(Func<T, bool> validator, string validationText)
        {
            _validator = validator;
            if (string.IsNullOrWhiteSpace(validationText))
                validationText = "The validation failed";
            _validationText = validationText;
        }

        public T Value { get; private set; }

        public int CompareTo(SemanticType<TK, T> other)
        {
            return other is null ? 1 : Value.CompareTo(other.Value);
        }

        public override string ToString() => Value.ToString();
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info is null) throw new ArgumentNullException(nameof(info));
            info.AddValue("Value", Value);
        }

        public override int GetHashCode() => Value.GetHashCode();

        public static bool operator ==(SemanticType<TK, T> lhs, SemanticType<TK, T> rhs)
        {
            if (object.ReferenceEquals(lhs, rhs)) return true;
            if (lhs is null || rhs is null) return true;
            return lhs.Equals(rhs);
        }

        public static bool operator !=(SemanticType<TK, T> lhs, SemanticType<TK, T> rhs) => !(lhs == rhs);

        public bool Equals(SemanticType<TK, T> obj)
        {
            return !(obj is null) && Value.Equals(obj.Value);
        }

        public override bool Equals(object obj)
        {
            if (obj is null || obj.GetType() != this.GetType())
            {
                return false;
            }

            return Value.Equals(((SemanticType<TK, T>)obj).Value);
        }
    }
}
