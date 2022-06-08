using System.Text;
using SkyWing.Binary;
using SkyWing.Math;

namespace JavaProtocol.packet;

using static SkyWing.Binary.BinaryStream;

public abstract class Packet
{
	protected BinaryStream? Buffer { get; }

	public Packet()
	{
		
	}
	
	public Packet(BinaryStream buffer)
	{
		Buffer = buffer;
	}
	
	public abstract int Pid();

	protected abstract void Encode();

	protected abstract void Decode();
	
	/*
	 * Packet data.
	 */

	public abstract string GetName();

	public abstract bool CanBeSentBeforeLogin();

	public abstract bool Handler();

	/*
	 * Data put methods.
	 */

    protected long? GetLong()
    {
        return Buffer?.ReadLong();
    }

    protected int? GetInt()
    {
        return Buffer?.ReadInt();
    }

    protected float? GetFloat()
    {
        return Buffer?.ReadFloat();
    }

    protected double? GetDouble()
    {
        return Buffer?.ReadDouble();
    }

    protected short? GetShort()
    {
        return Buffer?.ReadShort();
    }

    protected short? GetSignedShort()
    {
        return Buffer?.ReadSignedShort();
    }

    // TODO: Implement reading triads.
    // protected string? GetTriad()
    // {
    //     return Buffer?.ReadTriad();
    // }

    protected bool? GetBool()
    {
        return Buffer?.ReadByte() != 0;
    }

    protected byte? GetByte()
    {
        return Buffer?.ReadByte();
    }

    protected int GetSignedByte()
    {
        return (Buffer?.ReadByte() ?? new byte()) << 56 >> 56;
    }

    protected string? GetString()
    {
        return Encoding.UTF8.GetString(Buffer?.ReadBytes(GetVarInt()) ?? Array.Empty<byte>());
    }

    protected int GetVarInt()
    {
        return 0; // TODO: JavaBinary.ReadComputerVarInt(Buffer);
    }

    protected float? GetAngle()
    {
        return (GetByte() ?? 0) * 360 / 256f;
    }
    
    protected Vector3 GetPosition()
    {
        var position = GetLong() ?? 0;
        return new Vector3(
            position >> 38,
            position & 0xFFF,
            position << 26 >> 38
        );
    }

    protected bool Feof()
    {
        return Buffer?.Feof() ?? true;
    }
}

public abstract class InboundPacket : Packet
{
	protected override void Encode()
	{
		throw new NotImplementedException("Encode() cannot be called on an inbound packet.");
	}
}

public abstract class OutboundPacket : Packet
{
	protected override void Decode()
	{
		throw new NotImplementedException("Decode() cannot be called on an outbound packet.");
	}
}

public static class ClientPackets
{ 
	// Play
	public const int SpawnEntityPacket = 0x00;
	public const int SpawnExperienceOrbPacket = 0x01;
	public const int SpawnLivingEntityPacket = 0x02;
	public const int SpawnPaintingPacket = 0x03;
	public const int SpawnPlayerPacket = 0x04;
	public const int EntityAnimationPacket = 0x05;
	public const int StatisticsPacket = 0x06;
	public const int AcknowledgePlayerDiggingPacket = 0x07;
	public const int BlockBreakAnimationPacket = 0x08;
	public const int BlockEntityDataPacket = 0x09;
	public const int BlockActionPacket = 0x0a;
	public const int BlockChangePacket = 0x0b;
	public const int BossBarPacket = 0x0c;
	public const int ServerDifficultyPacket = 0x0d;
	public const int ChatMessagePacket = 0x0e;
	public const int TabCompletePacket = 0x0f;
	public const int DeclareCommandsPacket = 0x10;
	public const int WindowConfirmationPacket = 0x11;
	public const int CloseWindowPacket = 0x12;
	public const int WindowItemsPacket = 0x13;
	public const int WindowPropertyPacket = 0x14;
	public const int SetSlotPacket = 0x15;
	public const int SetCooldownPacket = 0x16;
	public const int PluginMessagePacket = 0x17;
	public const int NamedSoundEffectPacket = 0x18;
	public const int PlayDisconnectPacket = 0x19;
	public const int EntityStatusPacket = 0x1a;
	public const int ExplosionPacket = 0x1b;
	public const int UnloadChunkPacket = 0x1c;
	public const int ChangeGameStatePacket = 0x1d;
	public const int OpenHorseWindowPacket = 0x1e;
	public const int KeepAlivePacket = 0x1f;
	public const int ChunkDataPacket = 0x20;
	public const int EffectPacket = 0x21;
	public const int ParticlePacket = 0x22;
	public const int UpdateLightPacket = 0x23;
	public const int JoinGamePacket = 0x24;
	public const int MapPacket = 0x25;
	public const int TradeListPacket = 0x26;
	public const int EntityPositionPacket = 0x27;
	public const int EntityPositionAndRotationPacket = 0x28;
	public const int EntityRotationPacket = 0x29;
	public const int EntityMovementPacket = 0x2a;
	public const int VehicleMovePacket = 0x2b;
	public const int OpenBookPacket = 0x2c;
	public const int OpenWindowPacket = 0x2d;
	public const int OpenSignEditorPacket = 0x2e;
	public const int CraftRecipeResponsePacket = 0x2f;
	public const int PlayerAbilitiesPacket = 0x30;
	public const int CombatEventPacket = 0x31;
	public const int PlayerListPacket = 0x32;
	public const int FacePlayerPacket = 0x33;
	public const int PlayerPositionAndLookPacket = 0x34;
	public const int UnlockRecipesPacket = 0x35;
	public const int DestroyEntitiesPacket = 0x36;
	public const int RemoveEntityEffectPacket = 0x37;
	public const int ResourcePackSendPacket = 0x38;
	public const int RespawnPacket = 0x39;
	public const int EntityHeadLookPacket = 0x3a;
	public const int MultiBlockChangePacket = 0x3b;
	public const int SelectAdvancementTabPacket = 0x3c;
	public const int WorldBorderPacket = 0x3d;
	public const int CameraPacket = 0x3e;
	public const int HeldItemChangePacket = 0x3f;
	public const int UpdateViewPositionPacket = 0x40;
	public const int UpdateViewDistancePacket = 0x41;
	public const int SpawnPositionPacket = 0x42;
	public const int DisplayScoreboardPacket = 0x43;
	public const int EntityMetadataPacket = 0x44;
	public const int AttachEntityPacket = 0x45;
	public const int EntityVelocityPacket = 0x46;
	public const int EntityEquipmentPacket = 0x47;
	public const int SetExperiencePacket = 0x48;
	public const int UpdateHealthPacket = 0x49;
	public const int ScoreboardObjectivePacket = 0x4a;
	public const int SetPassengersPacket = 0x4b;
	public const int TeamsPacket = 0x4c;
	public const int UpdateScorePacket = 0x4d;
	public const int TimeUpdatePacket = 0x4e;
	public const int TitlePacket = 0x4f;
	public const int EntitySoundEffectPacket = 0x50;
	public const int SoundEffectPacket = 0x51;
	public const int StopSoundPacket = 0x52;
	public const int PlayerListHeaderAndFooterPacket = 0x53;
	public const int NbtQueryResponse = 0x54;
	public const int CollectItemPacket = 0x55;
	public const int EntityTeleportPacket = 0x56;
	public const int AdvancementsPacket = 0x57;
	public const int EntityPropertiesPacket = 0x58;
	public const int EntityEffectPacket = 0x59;
	public const int DeclareRecipesPacket = 0x5a;

	// Status

	// Login
	public const int LoginDisconnectPacket = 0x00;
	public const int EncryptionRequestPacket = 0x01;
	public const int LoginSuccessPacket = 0x02;
}

public static class ServerPackets
{
    // Play
    public const int TeleportConfirmPacket = 0x00;
    public const int QueryBlockNbtPacket = 0x01;
    public const int QueryEntityNbtPacket = 0x0d;
    public const int SetDifficultyPacket = 0x02;
    public const int ChatMessagePacket = 0x03;
    public const int ClientStatusPacket = 0x04;
    public const int ClientSettingsPacket = 0x05;
    public const int TabCompletePacket = 0x06;
    public const int WindowConfirmationPacket = 0x07;
    public const int ClickWindowButtonPacket = 0x08;
    public const int ClickWindowPacket = 0x09;
    public const int CloseWindowPacket = 0x0a;
    public const int PluginMessagePacket = 0x0b;
    public const int EditBookPacket = 0x0c;
    public const int InteractEntityPacket = 0x0e;
    public const int GenerateStructurePacket = 0x0f;
    public const int KeepAlivePacket = 0x10;
    public const int LockDifficultyPacket = 0x11;
    public const int PlayerPositionPacket = 0x12;
    public const int PlayerPositionAndRotationPacket = 0x13;
    public const int PlayerRotationPacket = 0x14;
    public const int PlayerMovementPacket = 0x15;
    public const int VehicleMovePacket = 0x16;
    public const int SteerBoatPacket = 0x17;
    public const int PickItemPacket = 0x18;
    public const int CraftRecipeRequestPacket = 0x19;
    public const int PlayerAbilitiesPacket = 0x1a;
    public const int PlayerDiggingPacket = 0x1b;
    public const int EntityActionPacket = 0x1c;
    public const int SteerVehiclePacket = 0x1d;
    public const int SetDisplayedRecipePacket = 0x1e;
    public const int SetRecipeBookStatePacket = 0x1f;
    public const int NameItemPacket = 0x20;
    public const int ResourcePackStatusPacket = 0x21;
    public const int AdvancementTabPacket = 0x22;
    public const int SelectTradePacket = 0x23;
    public const int SetBeaconEffectPacket = 0x24;
    public const int HeldItemChangePacket = 0x25;
    public const int UpdateCommandBlockPacket = 0x26;
    public const int UpdateCommandBlockMinecartPacket = 0x27;
    public const int CreativeInventoryActionPacket = 0x28;
    public const int UpdateJigsawBlockPacket = 0x29;
    public const int UpdateStructureBlockPacket = 0x2a;
    public const int UpdateSignPacket = 0x2b;
    public const int AnimationPacket = 0x2c;
    public const int SpectatePacket = 0x2d;
    public const int PlayerBlockPlacement = 0x2e;
    public const int UseItemPacket = 0x2f;

    // Status

    // Login
    public const int LoginStartPacket = 0x00;
    public const int EncryptionResponsePacket = 0x01;
}