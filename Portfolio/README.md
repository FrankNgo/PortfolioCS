# Frank Ngo Portfolio

##### A portfolio website built with ASP.NET Core MVC in Visual Studio.  05/16/18

## By Frank Ngo

# Description
This is my personal portfolio website. Users can register and leave comments on blog posts.

### User Stories
* an admin can
  * register/log in to an account
  * view all blog posts
  * create/edit/delete a blog post
  * create new blog posts
  * leave comment on blog posts
  * delete any post comment

* a user can
  * register/log in to an account
  * view all blog posts
  * leave comment on blog posts

* a guest can
  * view all blog posts

## Technologies Used
* HTML
* CSS
* Bootstrap
* Visual Studio
* C#
* ASP .NET Core MVC
* Visual Studios
* MySql
* MAMP

## Run the Application  

  * _Clone the github respository_
  ```
  $ git clone https://github.com/FrankNgo/PortfolioCS.git
  ```

  * _Install the .NET Framework and MAMP_

    .NET Core 1.1 SDK (Software Development Kit)

    .NET runtime.

    MAMP

    See https://www.learnhowtoprogram.com/c/getting-started-with-c/installing-c for instructions and links.

* _Start the Apache and MySql Servers in MAMP_

 move into the directory
 ```
 $ cd Portfolio/Portfolio/
 ```

*  _Setup the database_

  ```
  dotnet restore
  dotnet ef database update
  ```
*  _Run the program_
  ```
  dotnet run
  ```


### License

*MIT License*

Copyright (c) 2018 **_Frank Ngo_**

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.