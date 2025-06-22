# Commit Message Guide
I want every commit in *UltimateMultiTool* to be searchable, reversible, and self-explanatory.  
This guide sets the rules that shall be followed in the future (as I have made too many commit comments that are not proffessional or serious, and honestly are just bad)

## 1. High-level rules

1. **One purpose per commit.** If changed two unrelated things, then split them.
2. **Imperative** The summary must read like a command:  
   “Add diff viewer”, “Fix memory leak”, etc.
3. **No profanity or private jokes.** This is a public repo now.

---

### 2 Examples

    Added diff viewer panel #42
    Allows side-by-side visual diff of two files.  
    fix(diff viewer): close stream before returning handle (Link to issue)

---

## 3. Body section guidelines

* State **why** the change is necessary in one short paragraph.  
* Mention side effects or follow up work.  
* If the change relates to reading I am doing, cite the source, for example:  
  "Ref: C# in Depth ch 6 p 211 - closure capture fix."

## 4. Workflow checklist
 
1. Confirm unit tests pass: `dotnet test`.  
2. Commit with a clear message that follows this guide.  
3. Push or open a pull request.

---
