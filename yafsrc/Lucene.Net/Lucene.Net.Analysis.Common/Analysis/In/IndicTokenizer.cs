// Lucene version compatibility level 4.8.1
using J2N;
using YAF.Lucene.Net.Analysis.Util;
using YAF.Lucene.Net.Util;
using System;
using System.Globalization;
using System.IO;

namespace YAF.Lucene.Net.Analysis.In
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
    /// Simple Tokenizer for text in Indian Languages. </summary>
    /// @deprecated (3.6) Use <see cref="Standard.StandardTokenizer"/> instead. 
    [Obsolete("(3.6) Use StandardTokenizer instead.")]
    public sealed class IndicTokenizer : CharTokenizer
    {
        public IndicTokenizer(LuceneVersion matchVersion, AttributeFactory factory, TextReader input)
            : base(matchVersion, factory, input)
        {
        }

        public IndicTokenizer(LuceneVersion matchVersion, TextReader input)
            : base(matchVersion, input)
        {
        }

        override protected bool IsTokenChar(int c)
        {
            UnicodeCategory category = Character.GetType(c);

            return Character.IsLetter(c) ||
                category == UnicodeCategory.NonSpacingMark ||
                category == UnicodeCategory.Format ||
                category == UnicodeCategory.SpacingCombiningMark;
        }
    }
}