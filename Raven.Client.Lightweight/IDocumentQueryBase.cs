using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Raven.Abstractions.Data;
using Raven.Client.Document;

namespace Raven.Client
{
    /// <summary>
    ///   A query against a Raven index
    /// </summary>
    public interface IDocumentQueryBase<T, out TSelf>
        where TSelf : IDocumentQueryBase<T, TSelf>
    {
		/// <summary>
		/// Gets the document convention from the query session
		/// </summary>
		DocumentConvention DocumentConvention { get; }

        /// <summary>
        ///   Negate the next operation
        /// </summary>
        TSelf Not { get; }

		/// <summary>
		///   Negate the next operation
		/// </summary>
		void NegateNext();

        /// <summary>
        ///   Includes the specified path in the query, loading the document specified in that path
        /// </summary>
        /// <param name = "path">The path.</param>
        TSelf Include(string path);


        /// <summary>
        ///   This function exists solely to forbid in memory where clause on IDocumentQuery, because
        ///   that is nearly always a mistake.
        /// </summary>
        [Obsolete(
            @"
You cannot issue an in memory filter - such as Where(x=>x.Name == ""Ayende"") - on IDocumentQuery. 
This is likely a bug, because this will execute the filter in memory, rather than in RavenDB.
Consider using session.Query<T>() instead of session.LuceneQuery<T>. The session.Query<T>() method fully supports Linq queries, while session.LuceneQuery<T>() is intended for lower level API access.
If you really want to do in memory filtering on the data returned from the query, you can use: session.LuceneQuery<T>().ToList().Where(x=>x.Name == ""Ayende"")
"
            , true)]
        IEnumerable<T> Where(Func<T, bool> predicate);

        /// <summary>
        ///   This function exists solely to forbid in memory where clause on IDocumentQuery, because
        ///   that is nearly always a mistake.
        /// </summary>
        [Obsolete(
            @"
You cannot issue an in memory filter - such as Count(x=>x.Name == ""Ayende"") - on IDocumentQuery. 
This is likely a bug, because this will execute the filter in memory, rather than in RavenDB.
Consider using session.Query<T>() instead of session.LuceneQuery<T>. The session.Query<T>() method fully supports Linq queries, while session.LuceneQuery<T>() is intended for lower level API access.
If you really want to do in memory filtering on the data returned from the query, you can use: session.LuceneQuery<T>().ToList().Count(x=>x.Name == ""Ayende"")
"
            , true)]
        int Count(Func<T, bool> predicate);

        /// <summary>
        ///   This function exists solely to forbid in memory where clause on IDocumentQuery, because
        ///   that is nearly always a mistake.
        /// </summary>
        [Obsolete(
            @"
You cannot issue an in memory filter - such as Count() - on IDocumentQuery. 
This is likely a bug, because this will execute the filter in memory, rather than in RavenDB.
Consider using session.Query<T>() instead of session.LuceneQuery<T>. The session.Query<T>() method fully supports Linq queries, while session.LuceneQuery<T>() is intended for lower level API access.
If you really want to do in memory filtering on the data returned from the query, you can use: session.LuceneQuery<T>().ToList().Count()
"
            , true)]
        int Count();

        /// <summary>
        ///   Includes the specified path in the query, loading the document specified in that path
        /// </summary>
        /// <param name = "path">The path.</param>
        TSelf Include(Expression<Func<T, object>> path);

        /// <summary>
        ///   Takes the specified count.
        /// </summary>
        /// <param name = "count">The count.</param>
        /// <returns></returns>
        TSelf Take (int count);

        /// <summary>
        ///   Skips the specified count.
        /// </summary>
        /// <param name = "count">The count.</param>
        /// <returns></returns>
        TSelf Skip(int count);

        /// <summary>
        ///   Filter the results from the index using the specified where clause.
        /// </summary>
        /// <param name = "whereClause">The where clause.</param>
        TSelf Where(string whereClause);

        /// <summary>
        ///   Matches exact value
        /// </summary>
        /// <remarks>
        ///   Defaults to NotAnalyzed
        /// </remarks>
        TSelf WhereEquals(string fieldName, object value);

        /// <summary>
        ///   Matches exact value
        /// </summary>
        /// <remarks>
        ///   Defaults to allow wildcards only if analyzed
        /// </remarks>
        TSelf WhereEquals(string fieldName, object value, bool isAnalyzed);

        /// <summary>
        ///   Matches exact value
        /// </summary>
        TSelf WhereEquals (WhereEqualsParams whereEqualsParams);

        /// <summary>
        ///   Matches substrings of the field
        /// </summary>
        TSelf WhereContains(string fieldName, object value);

		/// <summary>
		///   Matches substrings of the field
		/// </summary>
		TSelf WhereContains(string fieldName, params object[] values);

		/// <summary>
		///   Matches substrings of the field
		/// </summary>
		TSelf WhereContains(string fieldName, IEnumerable<object> values);

        /// <summary>
        ///   Matches fields which starts with the specified value.
        /// </summary>
        /// <param name = "fieldName">Name of the field.</param>
        /// <param name = "value">The value.</param>
        TSelf WhereStartsWith (string fieldName, object value);

        /// <summary>
        ///   Matches fields which ends with the specified value.
        /// </summary>
        /// <param name = "fieldName">Name of the field.</param>
        /// <param name = "value">The value.</param>
        TSelf WhereEndsWith (string fieldName, object value);

        /// <summary>
        ///   Matches fields where the value is between the specified start and end, exclusive
        /// </summary>
        /// <param name = "fieldName">Name of the field.</param>
        /// <param name = "start">The start.</param>
        /// <param name = "end">The end.</param>
        TSelf WhereBetween (string fieldName, object start, object end);

        /// <summary>
        ///   Matches fields where the value is between the specified start and end, inclusive
        /// </summary>
        /// <param name = "fieldName">Name of the field.</param>
        /// <param name = "start">The start.</param>
        /// <param name = "end">The end.</param>
        TSelf WhereBetweenOrEqual (string fieldName, object start, object end);

        /// <summary>
        ///   Matches fields where the value is greater than the specified value
        /// </summary>
        /// <param name = "fieldName">Name of the field.</param>
        /// <param name = "value">The value.</param>
        TSelf WhereGreaterThan (string fieldName, object value);

        /// <summary>
        ///   Matches fields where the value is greater than or equal to the specified value
        /// </summary>
        /// <param name = "fieldName">Name of the field.</param>
        /// <param name = "value">The value.</param>
        TSelf WhereGreaterThanOrEqual (string fieldName, object value);

        /// <summary>
        ///   Matches fields where the value is less than the specified value
        /// </summary>
        /// <param name = "fieldName">Name of the field.</param>
        /// <param name = "value">The value.</param>
        TSelf WhereLessThan (string fieldName, object value);

        /// <summary>
        ///   Matches fields where the value is less than or equal to the specified value
        /// </summary>
        /// <param name = "fieldName">Name of the field.</param>
        /// <param name = "value">The value.</param>
        TSelf WhereLessThanOrEqual (string fieldName, object value);

        /// <summary>
        ///   Add an AND to the query
        /// </summary>
        TSelf AndAlso ();

        /// <summary>
        ///   Add an OR to the query
        /// </summary>
        TSelf OrElse();

        /// <summary>
        ///   Specifies a boost weight to the last where clause.
        ///   The higher the boost factor, the more relevant the term will be.
        /// </summary>
        /// <param name = "boost">boosting factor where 1.0 is default, less than 1.0 is lower weight, greater than 1.0 is higher weight</param>
        /// <returns></returns>
        /// <remarks>
        ///   http://lucene.apache.org/java/2_4_0/queryparsersyntax.html#Boosting%20a%20Term
        /// </remarks>
        TSelf Boost (decimal boost);

        /// <summary>
        ///   Specifies a fuzziness factor to the single word term in the last where clause
        /// </summary>
        /// <param name = "fuzzy">0.0 to 1.0 where 1.0 means closer match</param>
        /// <returns></returns>
        /// <remarks>
        ///   http://lucene.apache.org/java/2_4_0/queryparsersyntax.html#Fuzzy%20Searches
        /// </remarks>
        TSelf Fuzzy (decimal fuzzy);

        /// <summary>
        ///   Specifies a proximity distance for the phrase in the last where clause
        /// </summary>
        /// <param name = "proximity">number of words within</param>
        /// <returns></returns>
        /// <remarks>
        ///   http://lucene.apache.org/java/2_4_0/queryparsersyntax.html#Proximity%20Searches
        /// </remarks>
        TSelf Proximity (int proximity);

        /// <summary>
        ///   Filter matches to be inside the specified radius
        /// </summary>
        /// <param name = "radius">The radius.</param>
        /// <param name = "latitude">The latitude.</param>
        /// <param name = "longitude">The longitude.</param>
        TSelf WithinRadiusOf(double radius, double latitude, double longitude);

        /// <summary>
        ///   Sorts the query results by distance.
        /// </summary>
        TSelf SortByDistance();

        /// <summary>
        ///   Order the results by the specified fields
        /// </summary>
        /// <remarks>
        ///   The fields are the names of the fields to sort, defaulting to sorting by ascending.
        ///   You can prefix a field name with '-' to indicate sorting by descending or '+' to sort by ascending
        /// </remarks>
        /// <param name = "fields">The fields.</param>
        TSelf OrderBy(params string[] fields);

        /// <summary>
        ///   Instructs the query to wait for non stale results as of now.
        /// </summary>
        /// <returns></returns>
        TSelf WaitForNonStaleResultsAsOfNow();

        /// <summary>
        ///   Instructs the query to wait for non stale results as of now for the specified timeout.
        /// </summary>
        /// <param name = "waitTimeout">The wait timeout.</param>
        /// <returns></returns>
        TSelf WaitForNonStaleResultsAsOfNow(TimeSpan waitTimeout);

        /// <summary>
        ///   Instructs the query to wait for non stale results as of the cutoff date.
        /// </summary>
        /// <param name = "cutOff">The cut off.</param>
        /// <returns></returns>
        TSelf WaitForNonStaleResultsAsOf(DateTime cutOff);

        /// <summary>
        ///   Instructs the query to wait for non stale results as of the cutoff date for the specified timeout
        /// </summary>
        /// <param name = "cutOff">The cut off.</param>
        /// <param name = "waitTimeout">The wait timeout.</param>
        TSelf WaitForNonStaleResultsAsOf(DateTime cutOff, TimeSpan waitTimeout);

        /// <summary>
        ///   EXPERT ONLY: Instructs the query to wait for non stale results.
        ///   This shouldn't be used outside of unit tests unless you are well aware of the implications
        /// </summary>
        TSelf WaitForNonStaleResults();

        /// <summary>
        ///   EXPERT ONLY: Instructs the query to wait for non stale results for the specified wait timeout.
        ///   This shouldn't be used outside of unit tests unless you are well aware of the implications
        /// </summary>
        /// <param name = "waitTimeout">The wait timeout.</param>
        TSelf WaitForNonStaleResults(TimeSpan waitTimeout);

        /// <summary>
        ///   Adds an ordering for a specific field to the query
        /// </summary>
        /// <param name = "fieldName">Name of the field.</param>
        /// <param name = "descending">if set to <c>true</c> [descending].</param>
        TSelf AddOrder(string fieldName, bool descending);

        /// <summary>
        ///   Adds an ordering for a specific field to the query and specifies the type of field for sorting purposes
        /// </summary>
        /// <param name = "fieldName">Name of the field.</param>
        /// <param name = "descending">if set to <c>true</c> [descending].</param>
        /// <param name = "fieldType">the type of the field to be sorted.</param>
        TSelf AddOrder (string fieldName, bool descending, Type fieldType);

        /// <summary>
        ///   Simplified method for opening a new clause within the query
        /// </summary>
        /// <returns></returns>
        TSelf OpenSubclause ();

        /// <summary>
        ///   Simplified method for closing a clause within the query
        /// </summary>
        /// <returns></returns>
        TSelf CloseSubclause ();

        ///<summary>
        ///  Instruct the index to group by the specified fields using the specified aggregation operation
        ///</summary>
        ///<remarks>
        ///  This is only valid on dynamic indexes queries
        ///</remarks>
        TSelf GroupBy (AggregationOperation aggregationOperation, params string[] fieldsToGroupBy);
    }
}