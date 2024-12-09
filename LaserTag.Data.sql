USE [laser_tag]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 12/6/2024 12:29:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Attributes]    Script Date: 12/6/2024 12:29:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Attributes](
	[id] [nvarchar](450) NOT NULL,
	[name] [nvarchar](max) NOT NULL,
	[description] [nvarchar](max) NOT NULL,
	[code_name] [nvarchar](max) NOT NULL,
	[is_gun] [bit] NOT NULL,
 CONSTRAINT [PK_Attributes] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Configs]    Script Date: 12/6/2024 12:29:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Configs](
	[config_id] [nvarchar](450) NOT NULL,
	[name] [nvarchar](max) NOT NULL,
	[code_name] [nvarchar](max) NOT NULL,
	[config_typebase_id] [nvarchar](450) NOT NULL,
	[value1] [nvarchar](max) NOT NULL,
	[value2] [nvarchar](max) NOT NULL,
	[value3] [nvarchar](max) NOT NULL,
	[value4] [nvarchar](max) NOT NULL,
	[value5] [nvarchar](max) NOT NULL,
	[description] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Configs] PRIMARY KEY CLUSTERED 
(
	[config_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Hit_Logs]    Script Date: 12/6/2024 12:29:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Hit_Logs](
	[hit_log_id] [nvarchar](450) NOT NULL,
	[source_playerid] [nvarchar](450) NULL,
	[target_playerid] [nvarchar](450) NULL,
	[round_id] [nvarchar](450) NOT NULL,
	[hit_typebase_id] [nvarchar](450) NULL,
	[value] [int] NOT NULL,
 CONSTRAINT [PK_Hit_Logs] PRIMARY KEY CLUSTERED 
(
	[hit_log_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Matches]    Script Date: 12/6/2024 12:29:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Matches](
	[id] [nvarchar](450) NOT NULL,
	[date] [datetime2](7) NOT NULL,
	[stagebase_id] [nvarchar](450) NULL,
 CONSTRAINT [PK_Matches] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Player_Attributes]    Script Date: 12/6/2024 12:29:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Player_Attributes](
	[id] [nvarchar](450) NOT NULL,
	[playerid] [nvarchar](450) NULL,
	[attributeid] [nvarchar](450) NOT NULL,
	[value] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Player_Attributes] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Player_Matches]    Script Date: 12/6/2024 12:29:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Player_Matches](
	[player_match_id] [nvarchar](450) NOT NULL,
	[playerid] [nvarchar](450) NULL,
	[matchid] [nvarchar](450) NOT NULL,
	[teambase_id] [nvarchar](450) NULL,
 CONSTRAINT [PK_Player_Matches] PRIMARY KEY CLUSTERED 
(
	[player_match_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Player_Upgrades]    Script Date: 12/6/2024 12:29:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Player_Upgrades](
	[player_upgrade_id] [nvarchar](450) NOT NULL,
	[player_match_id] [nvarchar](450) NULL,
	[upgradeid] [nvarchar](450) NULL,
 CONSTRAINT [PK_Player_Upgrades] PRIMARY KEY CLUSTERED 
(
	[player_upgrade_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Players]    Script Date: 12/6/2024 12:29:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Players](
	[id] [nvarchar](450) NOT NULL,
	[name] [nvarchar](max) NOT NULL,
	[mac_gun] [nvarchar](max) NOT NULL,
	[mac_vest] [nvarchar](max) NOT NULL,
	[current_health] [int] NOT NULL,
	[current_bullet] [int] NOT NULL,
	[balance] [decimal](10, 2) NOT NULL,
 CONSTRAINT [PK_Players] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rounds]    Script Date: 12/6/2024 12:29:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rounds](
	[round_id] [nvarchar](450) NOT NULL,
	[date] [datetime2](7) NOT NULL,
	[round_stagebase_id] [nvarchar](450) NULL,
	[matchid] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_Rounds] PRIMARY KEY CLUSTERED 
(
	[round_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Shared_Bases]    Script Date: 12/6/2024 12:29:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Shared_Bases](
	[base_id] [nvarchar](450) NOT NULL,
	[group_id1] [nvarchar](450) NOT NULL,
	[base_name1] [nvarchar](max) NOT NULL,
	[base_name2] [nvarchar](max) NOT NULL,
	[base_name3] [nvarchar](max) NOT NULL,
	[base_name4] [nvarchar](max) NOT NULL,
	[base_name5] [nvarchar](max) NOT NULL,
	[sort] [int] NOT NULL,
	[description] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Shared_Bases] PRIMARY KEY CLUSTERED 
(
	[base_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Shared_Groups]    Script Date: 12/6/2024 12:29:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Shared_Groups](
	[group_id] [nvarchar](450) NOT NULL,
	[group_name1] [nvarchar](max) NOT NULL,
	[group_name2] [nvarchar](max) NOT NULL,
	[group_name3] [nvarchar](max) NOT NULL,
	[group_name4] [nvarchar](max) NOT NULL,
	[group_name5] [nvarchar](max) NOT NULL,
	[description] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Shared_Groups] PRIMARY KEY CLUSTERED 
(
	[group_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Shoot_Logs]    Script Date: 12/6/2024 12:29:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Shoot_Logs](
	[shoot_log_id] [nvarchar](450) NOT NULL,
	[playerid] [nvarchar](450) NOT NULL,
	[round_id] [nvarchar](450) NULL,
	[date] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Shoot_Logs] PRIMARY KEY CLUSTERED 
(
	[shoot_log_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Upgrade_Attributes]    Script Date: 12/6/2024 12:29:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Upgrade_Attributes](
	[id] [nvarchar](450) NOT NULL,
	[upgradeid] [nvarchar](450) NULL,
	[attributeid] [nvarchar](450) NOT NULL,
	[value] [int] NOT NULL,
 CONSTRAINT [PK_Upgrade_Attributes] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Upgrades]    Script Date: 12/6/2024 12:29:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Upgrades](
	[id] [nvarchar](450) NOT NULL,
	[name] [nvarchar](max) NOT NULL,
	[description] [nvarchar](max) NOT NULL,
	[price] [decimal](10, 2) NOT NULL,
 CONSTRAINT [PK_Upgrades] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240929100313_InitAppDbContext', N'8.0.8')
GO
INSERT [dbo].[Attributes] ([id], [name], [description], [code_name], [is_gun]) VALUES (N'1', N'Damage Value', N'Damage Value', N'damage_value', 1)
INSERT [dbo].[Attributes] ([id], [name], [description], [code_name], [is_gun]) VALUES (N'10', N'Fire Level', N'Fire Level', N'fire_level', 1)
INSERT [dbo].[Attributes] ([id], [name], [description], [code_name], [is_gun]) VALUES (N'11', N'Fire Duration', N'Fire Duration', N'fire_duration', 1)
INSERT [dbo].[Attributes] ([id], [name], [description], [code_name], [is_gun]) VALUES (N'12', N'Fire Value', N'Fire Value', N'fire_value', 1)
INSERT [dbo].[Attributes] ([id], [name], [description], [code_name], [is_gun]) VALUES (N'13', N'Fire Has True Damage', N'Fire Has True Damage', N'fire_has_true_damage', 1)
INSERT [dbo].[Attributes] ([id], [name], [description], [code_name], [is_gun]) VALUES (N'14', N'Poison Rate', N'Poison Rate', N'poison_rate', 1)
INSERT [dbo].[Attributes] ([id], [name], [description], [code_name], [is_gun]) VALUES (N'15', N'Poison Level', N'Poison Level', N'poison_level', 1)
INSERT [dbo].[Attributes] ([id], [name], [description], [code_name], [is_gun]) VALUES (N'16', N'Poison Duration', N'Poison Duration', N'poison_duration', 1)
INSERT [dbo].[Attributes] ([id], [name], [description], [code_name], [is_gun]) VALUES (N'17', N'Poison Value', N'Poison Value', N'poison_value', 1)
INSERT [dbo].[Attributes] ([id], [name], [description], [code_name], [is_gun]) VALUES (N'18', N'Deheal Level', N'Deheal Level', N'deheal_level', 1)
INSERT [dbo].[Attributes] ([id], [name], [description], [code_name], [is_gun]) VALUES (N'19', N'Deheal Duration', N'Deheal Duration', N'deheal_duration', 1)
INSERT [dbo].[Attributes] ([id], [name], [description], [code_name], [is_gun]) VALUES (N'2', N'Heal Value', N'Heal Value', N'healing_value', 1)
INSERT [dbo].[Attributes] ([id], [name], [description], [code_name], [is_gun]) VALUES (N'20', N'Deheal Heal Reduction', N'Deheal Heal Reduction', N'deheal_heal_reduction', 1)
INSERT [dbo].[Attributes] ([id], [name], [description], [code_name], [is_gun]) VALUES (N'21', N'Deheal Block Regen', N'Deheal Block Regen', N'deheal_block_regen', 1)
INSERT [dbo].[Attributes] ([id], [name], [description], [code_name], [is_gun]) VALUES (N'22', N'Silence Rate', N'Silence Rate', N'silence_rate', 1)
INSERT [dbo].[Attributes] ([id], [name], [description], [code_name], [is_gun]) VALUES (N'23', N'Silence Level', N'Silence Level', N'silence_level', 1)
INSERT [dbo].[Attributes] ([id], [name], [description], [code_name], [is_gun]) VALUES (N'24', N'Silence Duration', N'Silence Duration', N'silence_duration', 1)
INSERT [dbo].[Attributes] ([id], [name], [description], [code_name], [is_gun]) VALUES (N'25', N'Silence Armor Decrease', N'Silence Armor Decrease', N'silence_armor_decrease', 1)
INSERT [dbo].[Attributes] ([id], [name], [description], [code_name], [is_gun]) VALUES (N'3', N'Max Bullet', N'Max Bullet', N'bullet_max', 1)
INSERT [dbo].[Attributes] ([id], [name], [description], [code_name], [is_gun]) VALUES (N'4', N'Max SSketch Bullet', N'Max SSketch Bullet', N'ssketch_bullet_max', 1)
INSERT [dbo].[Attributes] ([id], [name], [description], [code_name], [is_gun]) VALUES (N'5', N'Bullet Reload Time', N'Bullet Reload Time', N'bullet_reload_time', 1)
INSERT [dbo].[Attributes] ([id], [name], [description], [code_name], [is_gun]) VALUES (N'50', N'Max Health', N'Max Health', N'health_max', 0)
INSERT [dbo].[Attributes] ([id], [name], [description], [code_name], [is_gun]) VALUES (N'51', N'Max Armor', N'Max Armor', N'armor_max', 0)
INSERT [dbo].[Attributes] ([id], [name], [description], [code_name], [is_gun]) VALUES (N'52', N'Armor Plus', N'Armor Plus', N'armor_plus', 0)
INSERT [dbo].[Attributes] ([id], [name], [description], [code_name], [is_gun]) VALUES (N'53', N'Armor Minus', N'Armor Minus', N'armor_minus', 0)
INSERT [dbo].[Attributes] ([id], [name], [description], [code_name], [is_gun]) VALUES (N'54', N'Extra Damage Receive', N'Extra Damage Receive', N'extra_damage_receive', 0)
INSERT [dbo].[Attributes] ([id], [name], [description], [code_name], [is_gun]) VALUES (N'55', N'Base Damage Vulnerability', N'Base Damage Vulnerability', N'base_damage_vul', 0)
INSERT [dbo].[Attributes] ([id], [name], [description], [code_name], [is_gun]) VALUES (N'56', N'Base Damage Resistance', N'Base Damage Resistance', N'base_damage_res', 0)
INSERT [dbo].[Attributes] ([id], [name], [description], [code_name], [is_gun]) VALUES (N'57', N'Bonus Damage Vulnerability', N'Bonus Damage Vulnerability', N'bonus_damage_vul', 0)
INSERT [dbo].[Attributes] ([id], [name], [description], [code_name], [is_gun]) VALUES (N'58', N'Bonus Damage Resistance', N'Bonus Damage Resistance', N'bonus_damage_res', 0)
INSERT [dbo].[Attributes] ([id], [name], [description], [code_name], [is_gun]) VALUES (N'6', N'SSketch Bullet Reload Time', N'SSketch Bullet Reload Time', N'ssketch_bullet_reload_time', 1)
INSERT [dbo].[Attributes] ([id], [name], [description], [code_name], [is_gun]) VALUES (N'7', N'Life Steal Value', N'Life Steal Value', N'life_steal_value', 1)
INSERT [dbo].[Attributes] ([id], [name], [description], [code_name], [is_gun]) VALUES (N'8', N'Has True Damage', N'Has True Damage', N'has_true_damage', 1)
INSERT [dbo].[Attributes] ([id], [name], [description], [code_name], [is_gun]) VALUES (N'9', N'Fire Rate', N'Fire Rate', N'fire_rate', 1)
GO
INSERT [dbo].[Configs] ([config_id], [name], [code_name], [config_typebase_id], [value1], [value2], [value3], [value4], [value5], [description]) VALUES (N'1', N'Default Player Damage', N'damage_value', N'CONF01', N'100', N'1', N'100', N'100', N'100', N'100')
INSERT [dbo].[Configs] ([config_id], [name], [code_name], [config_typebase_id], [value1], [value2], [value3], [value4], [value5], [description]) VALUES (N'10', N'Life Steal Value', N'life_steal_value', N'CONF01', N'0', N'1', N'0', N'0', N'0', N'0')
INSERT [dbo].[Configs] ([config_id], [name], [code_name], [config_typebase_id], [value1], [value2], [value3], [value4], [value5], [description]) VALUES (N'11', N'Has True Damage', N'has_true_damage', N'CONF01', N'0', N'1', N'0', N'0', N'0', N'0')
INSERT [dbo].[Configs] ([config_id], [name], [code_name], [config_typebase_id], [value1], [value2], [value3], [value4], [value5], [description]) VALUES (N'12', N'Fire Rate', N'fire_rate', N'CONF01', N'0', N'1', N'0', N'0', N'0', N'0')
INSERT [dbo].[Configs] ([config_id], [name], [code_name], [config_typebase_id], [value1], [value2], [value3], [value4], [value5], [description]) VALUES (N'13', N'Fire Duration', N'fire_duration', N'CONF01', N'0', N'1', N'0', N'0', N'0', N'0')
INSERT [dbo].[Configs] ([config_id], [name], [code_name], [config_typebase_id], [value1], [value2], [value3], [value4], [value5], [description]) VALUES (N'14', N'Fire Value', N'fire_value', N'CONF01', N'0', N'1', N'0', N'0', N'0', N'0')
INSERT [dbo].[Configs] ([config_id], [name], [code_name], [config_typebase_id], [value1], [value2], [value3], [value4], [value5], [description]) VALUES (N'15', N'Fire Has True Damage', N'fire_has_true_damage', N'CONF01', N'0', N'1', N'0', N'0', N'0', N'0')
INSERT [dbo].[Configs] ([config_id], [name], [code_name], [config_typebase_id], [value1], [value2], [value3], [value4], [value5], [description]) VALUES (N'16', N'Poison Rate', N'poison_rate', N'CONF01', N'0', N'1', N'0', N'0', N'0', N'0')
INSERT [dbo].[Configs] ([config_id], [name], [code_name], [config_typebase_id], [value1], [value2], [value3], [value4], [value5], [description]) VALUES (N'17', N'Poison Level', N'poison_level', N'CONF01', N'0', N'1', N'0', N'0', N'0', N'0')
INSERT [dbo].[Configs] ([config_id], [name], [code_name], [config_typebase_id], [value1], [value2], [value3], [value4], [value5], [description]) VALUES (N'18', N'Poison Duration', N'poison_duration', N'CONF01', N'0', N'1', N'0', N'0', N'0', N'0')
INSERT [dbo].[Configs] ([config_id], [name], [code_name], [config_typebase_id], [value1], [value2], [value3], [value4], [value5], [description]) VALUES (N'19', N'Poison Value', N'poison_value', N'CONF01', N'0', N'1', N'0', N'0', N'0', N'0')
INSERT [dbo].[Configs] ([config_id], [name], [code_name], [config_typebase_id], [value1], [value2], [value3], [value4], [value5], [description]) VALUES (N'2', N'Default Player Max Bullet', N'bullet_max', N'CONF01', N'30', N'1', N'30', N'30', N'30', N'30')
INSERT [dbo].[Configs] ([config_id], [name], [code_name], [config_typebase_id], [value1], [value2], [value3], [value4], [value5], [description]) VALUES (N'20', N'Deheal Level', N'deheal_level', N'CONF01', N'0', N'1', N'0', N'0', N'0', N'0')
INSERT [dbo].[Configs] ([config_id], [name], [code_name], [config_typebase_id], [value1], [value2], [value3], [value4], [value5], [description]) VALUES (N'21', N'Deheal Duration', N'deheal_duration', N'CONF01', N'0', N'1', N'0', N'0', N'0', N'0')
INSERT [dbo].[Configs] ([config_id], [name], [code_name], [config_typebase_id], [value1], [value2], [value3], [value4], [value5], [description]) VALUES (N'22', N'Deheal Heal Reduction', N'deheal_heal_reduction', N'CONF01', N'0', N'1', N'0', N'0', N'0', N'0')
INSERT [dbo].[Configs] ([config_id], [name], [code_name], [config_typebase_id], [value1], [value2], [value3], [value4], [value5], [description]) VALUES (N'23', N'Deheal Block Regen', N'deheal_block_regen', N'CONF01', N'0', N'1', N'0', N'0', N'0', N'0')
INSERT [dbo].[Configs] ([config_id], [name], [code_name], [config_typebase_id], [value1], [value2], [value3], [value4], [value5], [description]) VALUES (N'24', N'Silence Rate', N'silence_rate', N'CONF01', N'0', N'1', N'0', N'0', N'0', N'0')
INSERT [dbo].[Configs] ([config_id], [name], [code_name], [config_typebase_id], [value1], [value2], [value3], [value4], [value5], [description]) VALUES (N'25', N'Silence Level', N'silence_level', N'CONF01', N'0', N'1', N'0', N'0', N'0', N'0')
INSERT [dbo].[Configs] ([config_id], [name], [code_name], [config_typebase_id], [value1], [value2], [value3], [value4], [value5], [description]) VALUES (N'26', N'Silence Duration', N'silence_duration', N'CONF01', N'0', N'1', N'0', N'0', N'0', N'0')
INSERT [dbo].[Configs] ([config_id], [name], [code_name], [config_typebase_id], [value1], [value2], [value3], [value4], [value5], [description]) VALUES (N'27', N'Silence Armor Decrease', N'silence_armor_decrease', N'CONF01', N'0', N'1', N'0', N'0', N'0', N'0')
INSERT [dbo].[Configs] ([config_id], [name], [code_name], [config_typebase_id], [value1], [value2], [value3], [value4], [value5], [description]) VALUES (N'28', N'Max Armor', N'armor_max', N'CONF01', N'0', N'0', N'0', N'0', N'0', N'0')
INSERT [dbo].[Configs] ([config_id], [name], [code_name], [config_typebase_id], [value1], [value2], [value3], [value4], [value5], [description]) VALUES (N'29', N'Armor Plus', N'armor_plus', N'CONF01', N'0', N'0', N'0', N'0', N'0', N'0')
INSERT [dbo].[Configs] ([config_id], [name], [code_name], [config_typebase_id], [value1], [value2], [value3], [value4], [value5], [description]) VALUES (N'3', N'Player Fire Level', N'fire_level', N'CONF01', N'0', N'1', N'0', N'0', N'0', N'0')
INSERT [dbo].[Configs] ([config_id], [name], [code_name], [config_typebase_id], [value1], [value2], [value3], [value4], [value5], [description]) VALUES (N'30', N'Armor Minus', N'armor_minus', N'CONF01', N'0', N'0', N'0', N'0', N'0', N'0')
INSERT [dbo].[Configs] ([config_id], [name], [code_name], [config_typebase_id], [value1], [value2], [value3], [value4], [value5], [description]) VALUES (N'31', N'Extra Damage Receive', N'extra_damage_receive', N'CONF01', N'0', N'0', N'0', N'0', N'0', N'0')
INSERT [dbo].[Configs] ([config_id], [name], [code_name], [config_typebase_id], [value1], [value2], [value3], [value4], [value5], [description]) VALUES (N'32', N'Base Damage Vulnerability', N'base_damage_vul', N'CONF01', N'0', N'0', N'0', N'0', N'0', N'0')
INSERT [dbo].[Configs] ([config_id], [name], [code_name], [config_typebase_id], [value1], [value2], [value3], [value4], [value5], [description]) VALUES (N'33', N'Base Damage Resistance', N'base_damage_res', N'CONF01', N'0', N'0', N'0', N'0', N'0', N'0')
INSERT [dbo].[Configs] ([config_id], [name], [code_name], [config_typebase_id], [value1], [value2], [value3], [value4], [value5], [description]) VALUES (N'34', N'Bonus Damage Vulnerability', N'bonus_damage_vul', N'CONF01', N'0', N'0', N'0', N'0', N'0', N'0')
INSERT [dbo].[Configs] ([config_id], [name], [code_name], [config_typebase_id], [value1], [value2], [value3], [value4], [value5], [description]) VALUES (N'35', N'Bonus Damage Resistance', N'bonus_damage_res', N'CONF01', N'0', N'0', N'0', N'0', N'0', N'0')
INSERT [dbo].[Configs] ([config_id], [name], [code_name], [config_typebase_id], [value1], [value2], [value3], [value4], [value5], [description]) VALUES (N'4', N'Max Health', N'health_max', N'CONF01', N'10000', N'0', N'10000', N'10000', N'10000', N'10000')
INSERT [dbo].[Configs] ([config_id], [name], [code_name], [config_typebase_id], [value1], [value2], [value3], [value4], [value5], [description]) VALUES (N'5', N'Armor', N'armor_value', N'CONF01', N'50', N'0', N'50', N'50', N'50', N'50')
INSERT [dbo].[Configs] ([config_id], [name], [code_name], [config_typebase_id], [value1], [value2], [value3], [value4], [value5], [description]) VALUES (N'6', N'Heal Value', N'healing_value', N'CONF01', N'0', N'1', N'0', N'0', N'0', N'0')
INSERT [dbo].[Configs] ([config_id], [name], [code_name], [config_typebase_id], [value1], [value2], [value3], [value4], [value5], [description]) VALUES (N'7', N'Max SSketch Bullet', N'ssketch_bullet_max', N'CONF01', N'0', N'1', N'0', N'0', N'0', N'0')
INSERT [dbo].[Configs] ([config_id], [name], [code_name], [config_typebase_id], [value1], [value2], [value3], [value4], [value5], [description]) VALUES (N'8', N'Bullet Reload Time', N'bullet_reload_time', N'CONF01', N'0', N'1', N'0', N'0', N'0', N'0')
INSERT [dbo].[Configs] ([config_id], [name], [code_name], [config_typebase_id], [value1], [value2], [value3], [value4], [value5], [description]) VALUES (N'9', N'SSketch Bullet Reload Time', N'ssketch_bullet_reload_time', N'CONF01', N'0', N'1', N'0', N'0', N'0', N'0')
GO
INSERT [dbo].[Players] ([id], [name], [mac_gun], [mac_vest], [current_health], [current_bullet], [balance]) VALUES (N'1', N'Dung', N'B8-98-38-B6-15-7B', N'98-49-3E-3A-2D-40', 0, 0, CAST(0.00 AS Decimal(10, 2)))
INSERT [dbo].[Players] ([id], [name], [mac_gun], [mac_vest], [current_health], [current_bullet], [balance]) VALUES (N'2', N'Duy', N'48-CF-C8-10-A4-4B', N'47-3C-B5-A5-26-E2', 0, 0, CAST(0.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[Shared_Bases] ([base_id], [group_id1], [base_name1], [base_name2], [base_name3], [base_name4], [base_name5], [sort], [description]) VALUES (N'CONF01', N'CONF00', N'Default player attribute value', N'Default player attribute value', N'Default player attribute value', N'Default player attribute value', N'Default player attribute value', 0, N'Default player attribute value')
INSERT [dbo].[Shared_Bases] ([base_id], [group_id1], [base_name1], [base_name2], [base_name3], [base_name4], [base_name5], [sort], [description]) VALUES (N'CONF02', N'CONF00', N'default system value', N'default system value', N'default system value', N'default system value', N'default system value', 1, N'default system value')
INSERT [dbo].[Shared_Bases] ([base_id], [group_id1], [base_name1], [base_name2], [base_name3], [base_name4], [base_name5], [sort], [description]) VALUES (N'MTST01', N'MTST00', N'Matching', N'Matching', N'Matching', N'Matching', N'Matching', 0, N'Matching')
INSERT [dbo].[Shared_Bases] ([base_id], [group_id1], [base_name1], [base_name2], [base_name3], [base_name4], [base_name5], [sort], [description]) VALUES (N'MTST02', N'MTST00', N'Started', N'Started', N'Started', N'Started', N'Started', 1, N'Started')
INSERT [dbo].[Shared_Bases] ([base_id], [group_id1], [base_name1], [base_name2], [base_name3], [base_name4], [base_name5], [sort], [description]) VALUES (N'MTST03', N'MTST00', N'Finished', N'Finished', N'Finished', N'Finished', N'Finished', 2, N'Finished')
INSERT [dbo].[Shared_Bases] ([base_id], [group_id1], [base_name1], [base_name2], [base_name3], [base_name4], [base_name5], [sort], [description]) VALUES (N'RDST01', N'RDST00', N'Buy phase', N'Buy phase', N'Buy phase', N'Buy phase', N'Buy phase', 0, N'Buy phase')
INSERT [dbo].[Shared_Bases] ([base_id], [group_id1], [base_name1], [base_name2], [base_name3], [base_name4], [base_name5], [sort], [description]) VALUES (N'RDST02', N'RDST00', N'Battle phase', N'Battle phase', N'Battle phase', N'Battle phase', N'Battle phase', 1, N'Battle phase')
INSERT [dbo].[Shared_Bases] ([base_id], [group_id1], [base_name1], [base_name2], [base_name3], [base_name4], [base_name5], [sort], [description]) VALUES (N'RDST03', N'RDST00', N'Review', N'Review', N'Review', N'Review', N'Review', 2, N'Review')
INSERT [dbo].[Shared_Bases] ([base_id], [group_id1], [base_name1], [base_name2], [base_name3], [base_name4], [base_name5], [sort], [description]) VALUES (N'RDST04', N'RDST00', N'Finished', N'Finished', N'Finished', N'Finished', N'Finished', 3, N'Finished')
INSERT [dbo].[Shared_Bases] ([base_id], [group_id1], [base_name1], [base_name2], [base_name3], [base_name4], [base_name5], [sort], [description]) VALUES (N'TEAM01', N'TEAM00', N'Team 1', N'Team 1', N'Team 1', N'Team 1', N'Team 1', 0, N'Team 1')
INSERT [dbo].[Shared_Bases] ([base_id], [group_id1], [base_name1], [base_name2], [base_name3], [base_name4], [base_name5], [sort], [description]) VALUES (N'TEAM02', N'TEAM00', N'Team 2', N'Team 2', N'Team 2', N'Team 2', N'Team 2', 1, N'Team 2')
INSERT [dbo].[Shared_Bases] ([base_id], [group_id1], [base_name1], [base_name2], [base_name3], [base_name4], [base_name5], [sort], [description]) VALUES (N'TEAM03', N'TEAM00', N'Team 3', N'Team 3', N'Team 3', N'Team 3', N'Team 3', 3, N'Team 3')
INSERT [dbo].[Shared_Bases] ([base_id], [group_id1], [base_name1], [base_name2], [base_name3], [base_name4], [base_name5], [sort], [description]) VALUES (N'TEAM04', N'TEAM00', N'Team 4', N'Team 4', N'Team 4', N'Team 4', N'Team 4', 4, N'Team 4')
GO
INSERT [dbo].[Shared_Groups] ([group_id], [group_name1], [group_name2], [group_name3], [group_name4], [group_name5], [description]) VALUES (N'CONF00', N'Config type', N'Config type', N'Config type', N'Config type', N'Config type', N'Config type')
INSERT [dbo].[Shared_Groups] ([group_id], [group_name1], [group_name2], [group_name3], [group_name4], [group_name5], [description]) VALUES (N'MTST00', N'Match stage', N'Match stage', N'Match stage', N'Match stage', N'Match stage', N'Match stage')
INSERT [dbo].[Shared_Groups] ([group_id], [group_name1], [group_name2], [group_name3], [group_name4], [group_name5], [description]) VALUES (N'RDST00', N'Round stage', N'Round stage', N'Round stage', N'Round stage', N'Round stage', N'Round stage')
INSERT [dbo].[Shared_Groups] ([group_id], [group_name1], [group_name2], [group_name3], [group_name4], [group_name5], [description]) VALUES (N'TEAM00', N'Team name', N'Team name', N'Team name', N'Team name', N'Team name', N'Team name')
GO
ALTER TABLE [dbo].[Configs]  WITH CHECK ADD  CONSTRAINT [FK_Configs_Shared_Bases_config_typebase_id] FOREIGN KEY([config_typebase_id])
REFERENCES [dbo].[Shared_Bases] ([base_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Configs] CHECK CONSTRAINT [FK_Configs_Shared_Bases_config_typebase_id]
GO
ALTER TABLE [dbo].[Hit_Logs]  WITH CHECK ADD  CONSTRAINT [FK_Hit_Logs_Players_source_playerid] FOREIGN KEY([source_playerid])
REFERENCES [dbo].[Players] ([id])
GO
ALTER TABLE [dbo].[Hit_Logs] CHECK CONSTRAINT [FK_Hit_Logs_Players_source_playerid]
GO
ALTER TABLE [dbo].[Hit_Logs]  WITH CHECK ADD  CONSTRAINT [FK_Hit_Logs_Players_target_playerid] FOREIGN KEY([target_playerid])
REFERENCES [dbo].[Players] ([id])
GO
ALTER TABLE [dbo].[Hit_Logs] CHECK CONSTRAINT [FK_Hit_Logs_Players_target_playerid]
GO
ALTER TABLE [dbo].[Hit_Logs]  WITH CHECK ADD  CONSTRAINT [FK_Hit_Logs_Rounds_round_id] FOREIGN KEY([round_id])
REFERENCES [dbo].[Rounds] ([round_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Hit_Logs] CHECK CONSTRAINT [FK_Hit_Logs_Rounds_round_id]
GO
ALTER TABLE [dbo].[Hit_Logs]  WITH CHECK ADD  CONSTRAINT [FK_Hit_Logs_Shared_Bases_hit_typebase_id] FOREIGN KEY([hit_typebase_id])
REFERENCES [dbo].[Shared_Bases] ([base_id])
GO
ALTER TABLE [dbo].[Hit_Logs] CHECK CONSTRAINT [FK_Hit_Logs_Shared_Bases_hit_typebase_id]
GO
ALTER TABLE [dbo].[Matches]  WITH CHECK ADD  CONSTRAINT [FK_Matches_Shared_Bases_stagebase_id] FOREIGN KEY([stagebase_id])
REFERENCES [dbo].[Shared_Bases] ([base_id])
GO
ALTER TABLE [dbo].[Matches] CHECK CONSTRAINT [FK_Matches_Shared_Bases_stagebase_id]
GO
ALTER TABLE [dbo].[Player_Attributes]  WITH CHECK ADD  CONSTRAINT [FK_Player_Attributes_Attributes_attributeid] FOREIGN KEY([attributeid])
REFERENCES [dbo].[Attributes] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Player_Attributes] CHECK CONSTRAINT [FK_Player_Attributes_Attributes_attributeid]
GO
ALTER TABLE [dbo].[Player_Attributes]  WITH CHECK ADD  CONSTRAINT [FK_Player_Attributes_Players_playerid] FOREIGN KEY([playerid])
REFERENCES [dbo].[Players] ([id])
GO
ALTER TABLE [dbo].[Player_Attributes] CHECK CONSTRAINT [FK_Player_Attributes_Players_playerid]
GO
ALTER TABLE [dbo].[Player_Matches]  WITH CHECK ADD  CONSTRAINT [FK_Player_Matches_Matches_matchid] FOREIGN KEY([matchid])
REFERENCES [dbo].[Matches] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Player_Matches] CHECK CONSTRAINT [FK_Player_Matches_Matches_matchid]
GO
ALTER TABLE [dbo].[Player_Matches]  WITH CHECK ADD  CONSTRAINT [FK_Player_Matches_Players_playerid] FOREIGN KEY([playerid])
REFERENCES [dbo].[Players] ([id])
GO
ALTER TABLE [dbo].[Player_Matches] CHECK CONSTRAINT [FK_Player_Matches_Players_playerid]
GO
ALTER TABLE [dbo].[Player_Matches]  WITH CHECK ADD  CONSTRAINT [FK_Player_Matches_Shared_Bases_teambase_id] FOREIGN KEY([teambase_id])
REFERENCES [dbo].[Shared_Bases] ([base_id])
GO
ALTER TABLE [dbo].[Player_Matches] CHECK CONSTRAINT [FK_Player_Matches_Shared_Bases_teambase_id]
GO
ALTER TABLE [dbo].[Player_Upgrades]  WITH CHECK ADD  CONSTRAINT [FK_Player_Upgrades_Player_Matches_player_match_id] FOREIGN KEY([player_match_id])
REFERENCES [dbo].[Player_Matches] ([player_match_id])
GO
ALTER TABLE [dbo].[Player_Upgrades] CHECK CONSTRAINT [FK_Player_Upgrades_Player_Matches_player_match_id]
GO
ALTER TABLE [dbo].[Player_Upgrades]  WITH CHECK ADD  CONSTRAINT [FK_Player_Upgrades_Upgrades_upgradeid] FOREIGN KEY([upgradeid])
REFERENCES [dbo].[Upgrades] ([id])
GO
ALTER TABLE [dbo].[Player_Upgrades] CHECK CONSTRAINT [FK_Player_Upgrades_Upgrades_upgradeid]
GO
ALTER TABLE [dbo].[Rounds]  WITH CHECK ADD  CONSTRAINT [FK_Rounds_Matches_matchid] FOREIGN KEY([matchid])
REFERENCES [dbo].[Matches] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Rounds] CHECK CONSTRAINT [FK_Rounds_Matches_matchid]
GO
ALTER TABLE [dbo].[Rounds]  WITH CHECK ADD  CONSTRAINT [FK_Rounds_Shared_Bases_round_stagebase_id] FOREIGN KEY([round_stagebase_id])
REFERENCES [dbo].[Shared_Bases] ([base_id])
GO
ALTER TABLE [dbo].[Rounds] CHECK CONSTRAINT [FK_Rounds_Shared_Bases_round_stagebase_id]
GO
ALTER TABLE [dbo].[Shared_Bases]  WITH CHECK ADD  CONSTRAINT [FK_Shared_Bases_Shared_Groups_group_id1] FOREIGN KEY([group_id1])
REFERENCES [dbo].[Shared_Groups] ([group_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Shared_Bases] CHECK CONSTRAINT [FK_Shared_Bases_Shared_Groups_group_id1]
GO
ALTER TABLE [dbo].[Shoot_Logs]  WITH CHECK ADD  CONSTRAINT [FK_Shoot_Logs_Players_playerid] FOREIGN KEY([playerid])
REFERENCES [dbo].[Players] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Shoot_Logs] CHECK CONSTRAINT [FK_Shoot_Logs_Players_playerid]
GO
ALTER TABLE [dbo].[Shoot_Logs]  WITH CHECK ADD  CONSTRAINT [FK_Shoot_Logs_Rounds_round_id] FOREIGN KEY([round_id])
REFERENCES [dbo].[Rounds] ([round_id])
GO
ALTER TABLE [dbo].[Shoot_Logs] CHECK CONSTRAINT [FK_Shoot_Logs_Rounds_round_id]
GO
ALTER TABLE [dbo].[Upgrade_Attributes]  WITH CHECK ADD  CONSTRAINT [FK_Upgrade_Attributes_Attributes_attributeid] FOREIGN KEY([attributeid])
REFERENCES [dbo].[Attributes] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Upgrade_Attributes] CHECK CONSTRAINT [FK_Upgrade_Attributes_Attributes_attributeid]
GO
ALTER TABLE [dbo].[Upgrade_Attributes]  WITH CHECK ADD  CONSTRAINT [FK_Upgrade_Attributes_Upgrades_upgradeid] FOREIGN KEY([upgradeid])
REFERENCES [dbo].[Upgrades] ([id])
GO
ALTER TABLE [dbo].[Upgrade_Attributes] CHECK CONSTRAINT [FK_Upgrade_Attributes_Upgrades_upgradeid]
GO
