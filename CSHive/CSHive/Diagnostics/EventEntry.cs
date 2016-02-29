using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;

namespace CS.Diagnostics
{
    /// <summary>
    /// 日志事件的相关内容
    /// </summary>
    public class EventEntry
    {
        
        /// <summary>
		/// The default date time format for a formatter date values. 
		/// Default as Round-trip value; "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffffffzzz".
		/// </summary>
		[SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed.")]
        internal const string DefaultDateTimeFormat = "O";

        private static readonly int CurrentProcessId = ProcessPropertyAccess.GetCurrentProcessId();

        private readonly Guid providerId;

        private readonly int eventId;

        private readonly string formattedMessage;

        private readonly ReadOnlyCollection<object> payload;

        private readonly DateTimeOffset timestamp;

        //private readonly EventSchema schema;

        private readonly int processId;

        private readonly int threadId;

        private readonly Guid activityId;

        private readonly Guid relatedActivityId;

        /// <summary>
		/// Gets the id of the source originating the event.
		/// </summary>
		/// <value>The provider id.</value>
		public Guid ProviderId
        {
            get
            {
                return this.providerId;
            }
        }

        /// <summary>
        /// Gets the event id.
        /// </summary>
        /// <value>The event id.</value>
        public int EventId
        {
            get
            {
                return this.eventId;
            }
        }

        /// <summary>
        /// Gets the event payload.
        /// </summary>
        /// <value>The event payload.</value>
        public ReadOnlyCollection<object> Payload
        {
            get
            {
                return this.payload;
            }
        }

        /// <summary>
        /// Gets the timestamp of the event.
        /// </summary>
        /// <value>The timestamp of the event.</value>
        public DateTimeOffset Timestamp
        {
            get
            {
                return this.timestamp;
            }
        }

        ///// <summary>
        ///// Gets the event schema.
        ///// </summary>
        ///// <value>The event schema.</value>
        //public EventSchema Schema
        //{
        //    get
        //    {
        //        return this.schema;
        //    }
        //}

        /// <summary>
        /// Gets the formatted message.
        /// </summary>
        /// <value>
        /// The formatted message.
        /// </value>
        public string FormattedMessage
        {
            get
            {
                return this.formattedMessage;
            }
        }

        /// <summary>
        /// Gets the process id.
        /// </summary>
        /// <value>
        /// The process id.
        /// </value>
        public int ProcessId
        {
            get
            {
                return this.processId;
            }
        }

        /// <summary>
        /// Gets the thread id.
        /// </summary>
        /// <value>
        /// The thread id.
        /// </value>
        public int ThreadId
        {
            get
            {
                return this.threadId;
            }
        }

        /// <summary>
        /// Gets the activity id.
        /// </summary>
        /// <value>
        /// The activity id.
        /// </value>
        public Guid ActivityId
        {
            get
            {
                return this.activityId;
            }
        }

        /// <summary>
        /// Gets the related activity id.
        /// </summary>
        /// <value>
        /// The related activity id.
        /// </value>
        public Guid RelatedActivityId
        {
            get
            {
                return this.relatedActivityId;
            }
        }
    }

   
}