﻿namespace YAF.Lucene.Net.Search.Highlight
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

    /// <summary> Processes terms found in the original text, typically by applying some form 
    /// of mark-up to highlight terms in HTML search results pages.</summary>
    public interface IFormatter
    {
        /// <param name="originalText">The section of text being considered for markup</param>
        /// <param name="tokenGroup">contains one or several overlapping Tokens along with
        /// their scores and positions.</param>
        string HighlightTerm(string originalText, TokenGroup tokenGroup);
    }
}