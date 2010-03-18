use MIS2010Group1;
 
-- DUser data
insert into DUser(username,userPass,userFName,userLName) values 
('llace02','slim22','Ryan','Shepherd'),
('dmoore','hello1','Devin','Moore'),
('cturner','cturner1','Christina','Turner'),
('eshepher','eshep1','Edwin','Shepherd'),
('mjordan','Mjor23','Micheal','Jordan'),
('ohansen','ohans1','Ola','Hansen'),
('tsvends','Tsvends1','Tove','Svendson'),
('tbailey','tbailey','Tommy','Bailey'),
('dhypes','dhypes','Dan','Hypes'),
('nmelonas','nmelonas','Neo','Melonas');

-- Phone Data
insert into Phone(userID, phoneNum) values 
(1, '3215648901'),
(2, '5629085706'),
(3, '3215545869'),
(4, '3429876753'),
(5, '3211236784'),
(6, '6754536759'),
(7, '5673348753'),
(8, '3215648901'),
(9, '2469835576'),
(10,'7241198756');

-- Address Data
insert into Address(street1, city, state, ZIP) values 
('1215 Mason Road', 'Brooklyn', 'CO',26505),
('101 High Ground Street', 'Manhattan', 'NY',26505),
('224 Lakeview Drive', 'Brooklyn', 'CO',26505),
('123 Test Street','Testville','WV',26505),
('123 Elm Street','McLean','VA',26505),
('54 Possibly Elephantine Road','Baltimore','MD',26505),
('1215 Mason Road', 'Brooklyn', 'CO',26505),
('44883 Nottinghats Drive','Places','WA',26505);
insert into Address(street1, street2, city, state, ZIP) values 
('1232 Fallback Rd', 'Apartment B', 'S. Richmond Hill', 'DE',26505),
('577 Oatmeal Terrace','Apartment Q','Morgantown','WV',26505);

-- UserRoles Data
insert into UserRoles(roleName) values 
('Teacher'),
('Student'),
('Parent');

-- UserRole Data
insert into UserRole(userID,roleID) values 
(1,12),
(2,12),
(3,11),
(4,12),
(5,13),
(6,13),
(7,12),
(8,11),
(9,13),
(10,11),
(10,13);


-- Parent Data
insert into Parent(parentID) values 
(5),
(6),
(9),
(10);

-- Student Data
insert into Student(studentID,parentID) values 
(1,9),
(2,6),
(4,5),
(7,6);

-- Teacher Data
insert into Teacher(teacherID) values 
(3),
(8),
(10);


-- Homeroom Data
insert into Homeroom (classNumber,teacherID) values 
(4100, 8), 
(2302, 3),
(3601, 10),
(3602, 10);

-- StudentHomroom Data
insert into StudentHomeroom (studentID, homeroomID) values
(2,3),
(4,1),
(1,4),
(7,2);

-- AssignedDemerits Data
insert into AssignedDemerits (adTimestamp,teacherID, studentID, assignedDemeritWeight) values 
('11/1/09 13:00', 10, 4, 0.75),
('11/1/09 09:30', 8, 2, 0.40),
('08/1/09 14:10', 10, 7, 5.00),
('11/1/09 08:35', 3, 1, 0.75),
('12/11/09 12:00', 3, 7, 2.00),
('05/10/09 13:00', 8, 2, 1.00),
('01/17/10 11:00', 10, 1, .80);

--select * from AssignedDemerits

-- Demerits Data
insert into Demerits (demeritDescription) values 
('Electronics'),
('Candy'),
('Behavior'),
('Class Preparation'),
('Drugs');

-- DemeritList Data
insert into DemeritList (demeritID, assignedDemeritID) values 
(1,104),
(2,107),
(3,107),
(4,102),
(5,106),
(2,104),
(5,105),
(3,101),
(1,103),
(2,103),
(3,103),
(5,103);

-- Detention Data
insert into Detention (detentionDate) values 
('12/17/09'),
('11/17/09'),
('08/01/09'),
('01/21/10'),
('01/24/10');

-- StudentDetention Data
insert into StudentDetention (studentID, detentionID) values 
(1,1),
(2,2),
(4,3),
(7,4);
insert into StudentDetention (studentID, detentionID, served) values
(7,5,1);

-- Comments Data
insert into Comments (commentDesc, assignedDemeritID, commentTimestamp, userID, commentLink) values
('This is unfair!', 104, '11/11/09 13:00', 1, null),
('Why?', 102, '11/23/09 13:00', 6, null),
('This is my user comment', 106, '01/20/10 13:00', 2, null),
('Thanks for the vacation', 103, '01/22/09 13:00', 4, null),
('Thanks for the vacation', 103, '01/22/09 13:03', 5, 4);
