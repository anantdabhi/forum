﻿using YAF.Lucene.Net.Search;
using System.Xml;

namespace YAF.Lucene.Net.QueryParsers.Xml.Builders
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
    /// Builder for <see cref="DisjunctionMaxQuery"/>
    /// </summary>
    public class DisjunctionMaxQueryBuilder : IQueryBuilder
    {
        private readonly IQueryBuilder factory;

        public DisjunctionMaxQueryBuilder(IQueryBuilder factory)
        {
            this.factory = factory;
        }

        /// <summary>
        /// (non-Javadoc)
        /// @see org.apache.lucene.xmlparser.QueryObjectBuilder#process(org.w3c.dom.Element)
        /// </summary>
        public virtual Query GetQuery(XmlElement e)
        {
            float tieBreaker = DOMUtils.GetAttribute(e, "tieBreaker", 0.0f);
            DisjunctionMaxQuery dq = new DisjunctionMaxQuery(tieBreaker);
            dq.Boost = DOMUtils.GetAttribute(e, "boost", 1.0f);

            XmlNodeList nl = e.ChildNodes;
            for (int i = 0; i < nl.Count; i++)
            {
                XmlNode node = nl.Item(i);
                if (node is XmlElement queryElem)
                { // all elements are disjuncts.
                    Query q = factory.GetQuery(queryElem);
                    dq.Add(q);
                }
            }

            return dq;
        }
    }
}
