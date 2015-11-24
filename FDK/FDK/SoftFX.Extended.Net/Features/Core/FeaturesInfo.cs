namespace SoftFX.Extended
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Threading;
    using SoftFX.Extended.Errors;

    /// <summary>
    /// The class describes supported features.
    /// </summary>
    /// <typeparam name="TInfoProvider">The class describes supported features of a corresponding class.</typeparam>
    /// <typeparam name="TInfo">The class describes supported features of an instance.</typeparam>
    public class FeaturesInfo<TInfoProvider, TInfo>
        where TInfoProvider : IFeaturesInfoProvider<TInfo>, new()
        where TInfo : class
    {
        #region Members

        static readonly TInfoProvider InfoProvider = new TInfoProvider();

        FixProtocolVersion protocolVersion;
        TInfo info;

        #endregion

        internal FeaturesInfo()
        {
        }

        /// <summary>
        /// Constructs a new FeaturesInfo instance.
        /// </summary>
        /// <param name="protocolVersion">Can not be null.</param>
        /// <exception cref="System.ArgumentNullException">If 'protocolVersion' is null.</exception>
        protected FeaturesInfo(FixProtocolVersion protocolVersion)
        {
            if (protocolVersion == null)
                throw new ArgumentNullException("protocolVersion", "Protocol version can not be null.");

            this.protocolVersion = protocolVersion;
        }

        /// <summary>
        /// Gets object, which describes supported features of a corresponded class.
        /// </summary>
        public static TInfoProvider FeaturesProvider
        {
            get
            {
                return InfoProvider;
            }
        }

        /// <summary>
        /// Gets object, which describes supported features of a corresponded class instance.
        /// </summary>
        public TInfo Features
        {
            get
            {
                LazyInitializer.EnsureInitialized(ref this.info, () => InfoProvider.GetInfo(this.protocolVersion));
                return this.info;
            }
        }

        /// <summary>
        /// Gets protocol version of the instance.
        /// </summary>
        public FixProtocolVersion ProtocolVersion
        {
            get
            {
                return this.protocolVersion;
            }
            internal set
            {
                this.protocolVersion = value;
                this.info = null;
            }
        }

        /// <summary>
        /// The method throws exception, if a feature is not supported.
        /// </summary>
        /// <param name="message">Exception message; can not be null.</param>
        /// <param name="status">True, if a feature is supported, otherwise false</param>
        protected void ThrowIfFeatureNotSupported(string message, bool status)
        {
            if (!status)
                throw new UnsupportedFeatureException(message);
        }

        /// <summary>
        /// The method throws exception, if a feature is not supported.
        /// </summary>
        /// <param name="message">Exception message; can not be null.</param>
        /// <param name="feature">Unsupported feature name message; can not be null.</param>/// 
        /// <param name="status">True, if a feature is supported, otherwise false</param>
        protected void ThrowIfFeatureNotSupported(string message, string feature, bool status)
        {
            if (!status)
                throw new UnsupportedFeatureException(message, feature);
        }

        /// <summary>
        /// The method throws exception, if a property is not supported.
        /// </summary>
        /// <param name="property">Property name; can not be null.</param>
        /// <param name="status">True, if a feature is supported, otherwise false</param>
        protected void ThrowIfPropertyNotSupported(string property, bool status)
        {
            var message = string.Format("'{0}' property is not supported.", property);
            this.ThrowIfFeatureNotSupported(message, property, status);
        }

        /// <summary>
        /// The method throws exception, if a property is not supported.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyExpression"></param>
        protected void ThrowIfPropertyNotSupported<T>(Expression<Func<T>> propertyExpression)
        {
            if (propertyExpression == null)
                throw new ArgumentNullException("propertyExpression");

            if (propertyExpression.Body.NodeType != ExpressionType.MemberAccess)
                throw new ArgumentException("Expression type should be ExpressionType.MemberAccess");

            var memberExpresson = (MemberExpression)propertyExpression.Body;

            var name = string.Format("Is{0}Supported", memberExpresson.Member.Name);

            var propertyInfo = this.Features.GetType().GetProperty(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (propertyInfo == null)
#if DEBUG
                throw new InvalidOperationException(string.Format("Features do not contain property '{0}'.", name));
#else
                return;
#endif

            var isSupported = (bool)propertyInfo.GetValue(this.Features, null);

            this.ThrowIfPropertyNotSupported(memberExpresson.Member.Name, isSupported);
        }
    }
}
