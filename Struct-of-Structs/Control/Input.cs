using System;
using System.Collections.Concurrent;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Windows.Forms;

namespace Struct_of_Structs.Control
{
	public sealed class Input : Cleanup
	{
		private readonly ConcurrentDictionary<Keys, bool> status;
		private readonly ConcurrentDictionary<Keys, KeyEventHandler> handlers;
		private readonly Form form;

		public bool this[Keys key]
		{
			get { return status[key]; }
		}

		public Input(Form renderForm)
		{
			Contract.Assert(renderForm != null);

			status = new ConcurrentDictionary<Keys, bool>(3, 200);
			handlers = new ConcurrentDictionary<Keys, KeyEventHandler>(3, 200);

			foreach(var key in Enum.GetValues(typeof(Keys)).Cast<Keys>())
			{
				status[key] = false;
				handlers[key] = null;
			}

			form = renderForm;
			Hookup();
			OnCleanup += Unhook;
		}

		private void Hookup()
		{
			form.KeyUp += LinkFormOnKeyUp;
			form.KeyDown += LinkFormOnKeyDown;
			form.Disposed += (sender, args) =>
			{
				OnCleanup -= Unhook;
			};
		}

		private void Unhook()
		{
			form.KeyUp -= LinkFormOnKeyUp;
			form.KeyDown -= LinkFormOnKeyDown;
		}

		private void LinkFormOnKeyDown(object sender, KeyEventArgs keyEventArgs)
		{
			status[keyEventArgs.KeyCode] = true;
			var invoke = handlers[keyEventArgs.KeyData];
			if (invoke != null) invoke(form, keyEventArgs);
		}

		private void LinkFormOnKeyUp(object sender, KeyEventArgs keyEventArgs)
		{
			status[keyEventArgs.KeyCode] = false;
			var invoke = handlers[keyEventArgs.KeyData];
			if (invoke != null) invoke(form, keyEventArgs);
		}

		public void Subscribe(Keys key, KeyEventHandler keyEvent)
		{
			if(keyEvent != null)
				lock(handlers)
				{
					handlers[key] += keyEvent;
				}
		}
	}
}
