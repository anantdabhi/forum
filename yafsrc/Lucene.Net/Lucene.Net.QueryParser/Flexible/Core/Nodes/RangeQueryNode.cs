﻿namespace YAF.Lucene.Net.QueryParsers.Flexible.Core.Nodes
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
    /// This interface should be implemented by an <see cref="IQueryNode"/> that represents
    /// some kind of range query.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRangeQueryNode<T> : IRangeQueryNode, IFieldableNode
        where T : IFieldableNode /*IFieldValuePairQueryNode<?>*/
    {
        T LowerBound { get; }

        T UpperBound { get; }
    }

    /// <summary>
    /// LUCENENET specific interface for identifying a
    /// RangeQueryNode without specifying its generic closing type
    /// </summary>
    public interface IRangeQueryNode
    {
        bool IsLowerInclusive { get; }

        bool IsUpperInclusive { get; }
    }
}
