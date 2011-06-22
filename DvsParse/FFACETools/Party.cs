﻿using System;

namespace FFACETools
{
	public partial class FFACE
	{
		/// <summary>
		/// Wrapper class for Alliance information from FFACE
		/// </summary>
		public class PartyTools
		{
			#region Constructor

			/// <summary>
			/// Constructor
			/// </summary>
			/// <param name="instanceID">Instance ID generated by FFACE</param>
			public PartyTools(int instanceID)
			{
				_InstanceID = instanceID;

			} // @ public AllianceWrapper(int instanceID)

			#endregion

			#region Members

			/// <summary>
			/// Instance ID generated by FFACE
			/// </summary>
			private int _InstanceID { get; set; }

			/// <summary>
			/// Alliance leader's ID
			/// </summary>
			public int AllianceLeaderID
			{
				get { return GetPartyInformation().AllianceLeaderID; }

			} // @ public int LeaderID

			/// <summary>
			/// Party 0 leader's ID
			/// </summary>
			public int Party0LeaderID
			{
				get { return GetPartyInformation().Party0LeaderID; }

			} // @ public int Party0LeaderID

			/// <summary>
			/// Party 1 leader's ID
			/// </summary>
			public int Party1LeaderID
			{
				get { return GetPartyInformation().Party1LeaderID; }

			} // @ public int Party1LeaderID

			/// <summary>
			/// Party 2 leader's ID
			/// </summary>
			public int Party2LeaderID
			{
				get { return GetPartyInformation().Party2LeaderID; }

			} // @ public int Party2LeaderID

			/// <summary>
			/// Shows if the party members in party 0 are rendered on screen
			/// 
			/// UNKNOWN : boolean? bitmask per person? Always seems to return 0
			/// </summary>
			public int Party0Visible
			{
				get { return GetPartyInformation().Party0Visible; }

			} // @ public int Party0Visible

			/// <summary>
			/// Shows if the party members in party 1 are rendered on screen
			/// 
			/// UNKNOWN : boolean? bitmask per person?
			/// </summary>
			public int Party1Visible
			{
				get { return GetPartyInformation().Party1Visible; }

			} // @ public int Party1Visible

			/// <summary>
			/// Shows if the party members in party 2 are rendered on screen
			/// 
			/// UNKNOWN : boolean? bitmask per person?
			/// </summary>
			public int Party2Visible
			{
				get { return GetPartyInformation().Party2Visible; }

			} // @ public int Party2Visible

			/// <summary>
			/// Amount of members in Party 0
			/// </summary>
			public int Party0Count
			{
				get { return GetPartyInformation().Party0Count; }

			} // @ public int Party0Count

			/// <summary>
			/// Amount of members in Party 1
			/// </summary>
			public int Party1Count
			{
				get { return GetPartyInformation().Party1Count; }

			} // @ public int Party1Count

			/// <summary>
			/// Amount of members in Party 2
			/// </summary>
			public int Party2Count
			{
				get { return GetPartyInformation().Party2Count; }

			} // @ public int Party2Count

			/// <summary>
			/// If player has a party invitation waiting
			/// </summary>
			public bool Invited
			{
				get { return Convert.ToBoolean(GetPartyInformation().Invited); }

			} // @ public bool Invited

			#endregion

			#region Methods

			/// <summary>
			/// Gets the ALLIANCEINFO from FFACE
			/// </summary>
			private ALLIANCEINFO GetPartyInformation()
			{
				ALLIANCEINFO allianceInfo = new ALLIANCEINFO();
				GetAllianceInfo(_InstanceID, ref allianceInfo);

				return allianceInfo;

			} // @ public ALLIANCEINFO GetAllianceInformation()

			#endregion

		} // @ class AllianceWrapper
	} // @ public partial class FFACE
}
