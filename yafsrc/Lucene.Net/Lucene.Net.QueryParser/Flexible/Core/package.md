---
uid: Lucene.Net.QueryParsers.Flexible.Core
summary: *content
---

<!--
 Licensed to the Apache Software Foundation (ASF) under one or more
 contributor license agreements.  See the NOTICE file distributed with
 this work for additional information regarding copyright ownership.
 The ASF licenses this file to You under the Apache License, Version 2.0
 (the "License"); you may not use this file except in compliance with
 the License.  You may obtain a copy of the License at

     https://www.apache.org/licenses/LICENSE-2.0

 Unless required by applicable law or agreed to in writing, software
 distributed under the License is distributed on an "AS IS" BASIS,
 WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 See the License for the specific language governing permissions and
 limitations under the License.
-->


Core classes of the flexible query parser framework.

## Flexible Query Parser

This namespace contains the necessary classes to implement a query parser. 

A query parser is divided in at least 2 phases, text parsing and query building, and one optional phase called query processing. 

### First Phase: Text Parsing

The text parsing phase is performed by a text parser, which implements <xref:Lucene.Net.QueryParsers.Flexible.Core.Parser.ISyntaxParser> interface. A text parser is responsible to get a query string and convert it to a <xref:Lucene.Net.QueryParsers.Flexible.Core.Nodes.IQueryNode> tree, which is an object structure that represents the elements defined in the query string. 

### Second (optional) Phase: Query Processing

The query processing phase is performed by a query processor, which implements <xref:Lucene.Net.QueryParsers.Flexible.Core.Processors.QueryNodeProcessor>. A query processor is responsible to perform any processing on a <xref:Lucene.Net.QueryParsers.Flexible.Core.Nodes.IQueryNode> tree. This phase is optional and is used only if an extra processing, validation, query expansion, etc needs to be performed in a <xref:Lucene.Net.QueryParsers.Flexible.Core.Nodes.IQueryNode> tree. The <xref:Lucene.Net.QueryParsers.Flexible.Core.Nodes.IQueryNode> tree can be either be generated by a text parser or programmatically created. 

### Third Phase: Query Building

The query building phase is performed by a query builder, which implements <xref:Lucene.Net.QueryParsers.Flexible.Core.Builders.IQueryBuilder{Lucene.Net.QueryParsers.Flexible.Core.Nodes.IQueryNode}>. A query builder is responsible to convert a <xref:Lucene.Net.QueryParsers.Flexible.Core.Nodes.IQueryNode> tree into an arbitrary object, which is usually used to be executed against a search index. 