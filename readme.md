
We stumbled across a strange problem today whilst investigating a failing unit test.
We stepped through the code with the debugger and were confused by a line of code
similar to the following:

```csharp
_things = GetThings() ?? new List<Thing>();
```

GetThings() returns a List of Thing. The null coalescing operator is used to
create an empty List of Thing in the event that GetThings() returns null.
Therefore, it should not be possible for _things to be null after stepping
over the above line of code. But it was!

Later, I tried to reproduce this in a small test program. This little project is the result.
After much experimentation, I have managed to reproduce this with just a few lines of code.
The key things to reproduce the problem seem to be the following:

* The assignment needs to be to a member variable (_things).
* It does not behave oddly with a string. It does behave oddly with a List of string. I have not tried other types.
* There must be another line of code after the assignment to _things.

To see the problem, set a breakpoint on the line that assigns to _things. Step over this line of code.
Observe that _things is still null. Note that after stepping over the next line of code (the assignment to flag),
_things now contains the correct value (a List object containing 2 things).

```csharp
private static List<Thing> GetThings()
{
	return new List<Thing>
		{
			new Thing {Prop1 = "ABC", Prop2 = 123},
			new Thing {Prop1 = "DEF", Prop2 = 456}
		};
}

private List<Thing> _things;

[Test]
public void DebuggerStrangenessInvolvingTheNullCoalescingOperatorAndAMemberVariable()
{
	_things = GetThings() ?? new List<Thing>();
	var flag = true;
}
```

![Screenshot](https://raw.github.com/taylorjg/NullCoalescingExperiment/master/Images/NullCoalescingExperiment.png)
