using Sandbox;
using Sandbox.Citizen;
using Sandbox.UI;
using System.Xml.Linq;

public partial class Voter : Actor
{
	/// <summary>
	///  Who this voter is going to vote
	/// </summary>
	public Candidate Pick { get; private set; }

	protected override void OnStart()
	{
		base.OnStart();

		Pick = ElectionsManager.RandomCandidate();
	}

	public override void Talk( Player target )
	{
		base.Talk( target );

		var randomMessage = ElectionsManager.CleanMessage( Game.Random.FromList( InteractPhrases ), Pick, out var isAboutOpponent );
		var speechSpeed = 30f;
		var waitDuration = 2f;
		SpeechUI.AddSpeech( FullName, randomMessage, speechSpeed, waitDuration, GameObject, Gender );

		LookingTo = target;
		var talkDuration = randomMessage.Count() / speechSpeed;
		var duration = talkDuration + waitDuration;
		StopTalking = talkDuration;
		StopLooking = duration;
		Interaction.InteractionCooldown = duration;

		if ( isAboutOpponent )
			AngryFace();
		else
			HappyFace();
	}

	public override void StopLook()
	{
		base.StopLook();
		NeutralFace();
	}
}
