﻿namespace YAF.Lucene.Net.QueryParsers.Surround.Query
{
    /*
     * Licensed to the Apache Software Foundation (ASF) under one or more
     * contributor license agreements.  See the NOTICE file distributed with
     * this work for additional information regarding copyright ownership.
     * The ASF licenses this file to You under the Apache License, Version 2.0
     * (the "License"); you may not use this file except in compliance with
     * the License.  You may obtain a copy of the License at
     *
     *     https://www.apache.org/licenses/LICENSE-2.0
     *
     * Unless required by applicable law or agreed to in writing, software
     * distributed under the License is distributed on an "AS IS" BASIS,
     * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
     * See the License for the specific language governing permissions and
     * limitations under the License.
     */

    /// <summary>
    /// Interface for queries that can be nested as subqueries
    /// into a span near.
    /// </summary>
    public interface IDistanceSubQuery
    {
        /// <summary>
        /// When <see cref="DistanceSubQueryNotAllowed()"/> returns non null, the reason why the subquery
        /// is not allowed as a distance subquery is returned.
        /// <para/>When <see cref="DistanceSubQueryNotAllowed()"/> returns null <see cref="AddSpanQueries(SpanNearClauseFactory)"/> can be used
        /// in the creation of the span near clause for the subquery.
        /// </summary>
        string DistanceSubQueryNotAllowed();

        void AddSpanQueries(SpanNearClauseFactory sncf);
    }
}
