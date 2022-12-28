# CommentTreeBuilder
Builds a comment tree from a heap of related comments

Example

(>2 means that post has an answer with id 2)

Input data:
```
Post 1 >2 >3
Post 2 >4
Post 3 >4
Post 4
Post 5
```
(>2 >3 means that post has answers with id 2 and id 3 respectively)
Output data:
```
Post 1
 L_Post 2
  L_Post 4
 L_Post 3
  L_Post 4
Post 5
```
