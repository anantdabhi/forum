﻿using YAF.Lucene.Net.QueryParsers.Flexible.Core.Nodes;

namespace YAF.Lucene.Net.QueryParsers.Flexible.Standard.Nodes
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
    /// A <see cref="BooleanModifierNode"/> has the same behaviour as
    /// <see cref="ModifierQueryNode"/>, it only indicates that this modifier was added by
    /// <see cref="Processors.GroupQueryNodeProcessor"/> and not by the user. 
    /// </summary>
    /// <seealso cref="ModifierQueryNode"/>
    public class BooleanModifierNode : ModifierQueryNode
    {
        public BooleanModifierNode(IQueryNode node, Modifier mod)
            : base(node, mod)
        {
        }
    }
}
