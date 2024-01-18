CREATE TABLE [Store] 
(
  [ID]                int          NOT NULL IDENTITY(1, 1) PRIMARY KEY,
  [Name]          nvarchar(255) NOT NULL
);

CREATE TABLE [Delivery] 
(
  [ID]                  int           NOT NULL IDENTITY(1, 1) PRIMARY KEY,
  [Name]               nvarchar(255) NOT NULL
);

CREATE TABLE [Station] 
(
  [ID]      int   NOT NULL IDENTITY(1, 1) PRIMARY KEY,
  [Name]    nvarchar(255) NOT NULL
);

CREATE TABLE [MenuItem] 
(
  [ID]                  int   NOT NULL IDENTITY(1, 1) PRIMARY KEY,
  [Name]                nvarchar(255) NOT NULL,
  [Price]               decimal(10, 2) NOT NULL,
  [Description]         text,
  [Station_ID]                    int
);

CREATE TABLE [Order] 
(
  [ID]            int           NOT NULL IDENTITY(1, 1) PRIMARY KEY,
  [Name]          nvarchar(255)  NOT NULL,
  [Delivery_ID]         int,
  [Store_ID]            int,
  [Total_Price]         decimal(10, 2) NOT NULL,
  [Complete]            bit NOT NULL DEFAULT 0,
  [Time_Arrived]        datetime NOT NULL
);

CREATE TABLE [OrderedItems] 
(
  [ID]            int           NOT NULL IDENTITY(1, 1) PRIMARY KEY,
  [Menu_Item_ID]          int,
  [Order_ID]              int NOT NULL,
  [Quantity]              int NOT NULL,
  [Complete]              bit NOT NULL DEFAULT 0
);

ALTER TABLE [MenuItem] ADD CONSTRAINT [MenuItem_Fk_Station]         FOREIGN KEY ([Station_ID])         REFERENCES [Station] ([ID])         ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE [Order] ADD CONSTRAINT [Order_Fk_Delivery]  FOREIGN KEY ([Delivery_ID])  REFERENCES [Delivery] ([ID])  ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE [Order] ADD CONSTRAINT [Order_Fk_Store] FOREIGN KEY ([Store_ID]) REFERENCES [Store] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE [OrderedItems] ADD CONSTRAINT [OrderedItems_Fk_MenuItem]   FOREIGN KEY ([Menu_Item_ID])   REFERENCES [MenuItem] ([ID])   ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE [OrderedItems] ADD CONSTRAINT [OrderedItems_Fk_Order] FOREIGN KEY ([Order_ID]) REFERENCES [Order] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;
