//+---------------------------------------------------------------------------
//  Microsoft Virtual Server 
//  Copyright (c) 2007 Microsoft Corporation
//
//  File:       vhdmount.h
//
//  Contents:   VHDMount API Prototypes and Definitions
//
//----------------------------------------------------------------------------


#ifndef _VHDMOUNTAPI_H
#define _VHDMOUNTAPI_H

#ifdef __cplusplus
extern "C" {
#endif

#ifndef WINAPI
#define WINAPI __stdcall
#endif

#ifndef IN
#define IN
#endif

#ifndef OUT
#define OUT
#endif

#ifndef OPTIONAL
#define OPTIONAL
#endif

//
// VHD Mount API Flags
//
typedef enum _VHD_FLAGS {
	VHD_NORMAL,
	VHD_NW_MAPPED,			// Unused
	VHD_MOUNT_AS_READONLY,	// Unused
	VHD_FORCE_UNMOUNT
} VHD_FLAGS;

/*++
MountVHD

Routine Description:
	Mounts the VHD file as Virtual Scsi Disk.

Arguments:	
	VHDFileName	: VHD file name including path, null terminated wide char string.
	Flags		: Use VHD_NORMAL.

Return Value:
	ERROR_SUCCESS on successful mount. On failure, standard Windows error codes are returned.
--*/
DWORD
WINAPI 
MountVHD(
		 IN LPCWSTR VHDFileName,
		 IN ULONG  Flags);

/*++
UnmountVHD

Routine Description:
	Unmounts the Virtual SCSI Disk as specified by the given VHD File Name.

Arguments:
	VHDFileName	: VHD file name including path, null terminated wide char string.
	Flags		: Currently supports two options:
					VHD_NORMAL 			This queries the Plug and Play manager for safe 
										removal of the Virtual SCSI Disk. 
					VHD_FORCE_UNMOUNT	Force unmount of the Virtual SCSI Disk without sending any device eject
										notification. This could result in a device disappeared message getting 
										logged in the event viewer.

Return Value:
	ERROR_SUCCESS on successful unmount. On failure, standard Windows error codes are returned.
--*/
DWORD
WINAPI 
UnmountVHD(
		   IN LPCWSTR VHDFileName,
		   IN ULONG  Flags);

/*++
GetSCSIAddress

Routine Description:
	Returns the SCSI Address for the disk mounted using MountVHD routine.

Arguments:
	VHDFileName			: VHD file name including path, null terminated wide char string.
	Flags				: Use VHD_NORMAL.
	SCSIAddress			: A pointer to a buffer that receives the SCSI Address for the specified disk.
						  The buffer size should be large enough to contain MAX_PATH + 1 wide characters.
	SCSIAddressLength	: Size of the buffer being passed in WCHARs.
	
Return Value:
	ERROR_SUCCESS on successful retrieval of the SCSI address. On failure, standard Windows error codes
	are returned.
--*/
DWORD
WINAPI 
GetSCSIAddress(
			   IN LPCWSTR VHDFileName,
			   IN ULONG  Flags,
			   IN ULONG	 SCSIAddressLength,
			   OUT PWCHAR SCSIAddress);


#ifdef __cplusplus
}       // extern "C" 
#endif

#endif //_VHDMOUNTAPI_H

