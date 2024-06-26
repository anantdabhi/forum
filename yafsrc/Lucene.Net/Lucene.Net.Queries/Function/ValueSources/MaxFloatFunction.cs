﻿// Lucene version compatibility level 4.8.1
using System;

namespace YAF.Lucene.Net.Queries.Function.ValueSources
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
    /// <see cref="MaxSingleFunction"/> returns the max of it's components.
    /// <para/>
    /// NOTE: This was MaxFloatFunction in Lucene
    /// </summary>
    public class MaxSingleFunction : MultiSingleFunction
    {
        public MaxSingleFunction(ValueSource[] sources)
            : base(sources)
        {
        }

        override protected string Name => "max";

        override protected float Func(int doc, FunctionValues[] valsArr)
        {
            if (valsArr.Length == 0)
            {
                return 0.0f;
            }
            float val = float.NegativeInfinity;
            foreach (FunctionValues vals in valsArr)
            {
                val = Math.Max(vals.SingleVal(doc), val);
            }
            return val;
        }
    }
}