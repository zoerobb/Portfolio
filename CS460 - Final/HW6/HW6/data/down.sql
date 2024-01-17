ALTER TABLE [MenuItem] DROP CONSTRAINT [MenuItem_Fk_Station];

ALTER TABLE [Order] DROP CONSTRAINT [Order_Fk_Delivery];
ALTER TABLE [Order] DROP CONSTRAINT [Order_Fk_Store];

ALTER TABLE [OrderedItems] DROP CONSTRAINT [OrderedItems_Fk_MenuItem];
ALTER TABLE [OrderedItems] DROP CONSTRAINT [OrderedItems_Fk_Order];

DROP TABLE [Store];
DROP TABLE [Delivery];
DROP TABLE [Station];
DROP TABLE [MenuItem];
DROP TABLE [Order];
DROP TABLE [OrderedItems];
