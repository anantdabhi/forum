﻿using System;
using System.Runtime.CompilerServices;

namespace YAF.Lucene.Net.Util
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

    public static class WAH8DocIdSetExtensions
    {
        /// <summary>
        /// Return the number of documents in this <see cref="Lucene.Net.Search.DocIdSet"/> in constant time. </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [Obsolete("Use Cardinality property instead. This extension method will be removed in 4.8.0 release candidate."), System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public static int Cardinality(this WAH8DocIdSet set)
        {
            return set.Cardinality;
        }
    }
}
