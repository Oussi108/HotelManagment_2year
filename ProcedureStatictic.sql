
alter proc showstateroombymonth @d1 date ,@d2 date
as
begin
select R.id,R.Description,R.PriceD,
abs(sum(CASE
    WHEN Rs.StartDate>@d1 and Rs.EndDate<@d2 THEN DATEDIFF(DAY,Rs.EndDate ,Rs.StartDate) 
    WHEN Rs.StartDate>@d1 and Rs.EndDate>@d2 THEN DATEDIFF(DAY,Rs.EndDate ,@d2) 
    WHEN Rs.StartDate<@d1 and Rs.EndDate<@d2 THEN DATEDIFF(DAY, @d1,Rs.EndDate) 
    WHEN Rs.StartDate<@d1 and Rs.EndDate>@d2 THEN DATEDIFF(DAY, @d2,@d1) 
    ELSE -1 end ))
     as 'Days' , abs(sum(CASE
    WHEN Rs.StartDate>@d1 and Rs.EndDate<@d2 THEN DATEDIFF(DAY,Rs.EndDate ,Rs.StartDate) 
    WHEN Rs.StartDate>@d1 and Rs.EndDate>@d2 THEN DATEDIFF(DAY,Rs.EndDate ,@d2) 
    WHEN Rs.StartDate<@d1 and Rs.EndDate<@d2 THEN DATEDIFF(DAY, @d1,Rs.EndDate) 
    WHEN Rs.StartDate<@d1 and Rs.EndDate>@d2 THEN DATEDIFF(DAY, @d2,@d1) 
    ELSE -1 end )*R.PriceD) 'Total income'
from Rooms R join Reservations Rs on 
R.Id = Rs.RoomId
where R.Id in (
select RoomId from Reservations r
where r.StartDate between @d1 and @d2
 or  r.EndDate between @d1 and @d2
 or  (( @d1 between r.StartDate and r.EndDate)and(@d2 between r.StartDate and r.EndDate))
)
Group by R.id,R.Description,R.PriceD
end
'1999-1-1' to '2022-1-1'

DECLARE @days int

select R.id,R.Description,R.PriceD,
abs(sum(CASE
    WHEN Rs.StartDate>'1999-1-1' and Rs.EndDate<'2022-1-1' THEN DATEDIFF(DAY,Rs.EndDate ,Rs.StartDate) 
    WHEN Rs.StartDate>'1999-1-1' and Rs.EndDate>'2022-1-1' THEN DATEDIFF(DAY,Rs.EndDate ,'2022-1-1') 
    WHEN Rs.StartDate<'1999-1-1' and Rs.EndDate<'2022-1-1' THEN DATEDIFF(DAY, '1999-1-1',Rs.EndDate) 
    WHEN Rs.StartDate<'1999-1-1' and Rs.EndDate>'2022-1-1'THEN DATEDIFF(DAY, '2022-1-1','1999-1-1') 
    ELSE -1 end ))
     as 'Days' , abs(sum(CASE
    WHEN Rs.StartDate>'1999-1-1' and Rs.EndDate<'2022-1-1' THEN DATEDIFF(DAY,Rs.EndDate ,Rs.StartDate) 
    WHEN Rs.StartDate>'1999-1-1' and Rs.EndDate>'2022-1-1' THEN DATEDIFF(DAY,Rs.EndDate ,'2022-1-1') 
    WHEN Rs.StartDate<'1999-1-1' and Rs.EndDate<'2022-1-1' THEN DATEDIFF(DAY, '1999-1-1',Rs.EndDate) 
    WHEN Rs.StartDate<'1999-1-1' and Rs.EndDate>'2022-1-1'THEN DATEDIFF(DAY, '2022-1-1','1999-1-1') 
    ELSE -1 end )*R.PriceD) 'Total income'
from Rooms R join Reservations Rs on 
R.Id = Rs.RoomId
where R.Id in (
select RoomId from Reservations r
where r.StartDate between '1999-1-1' and '2022-1-1'
 or  r.EndDate between '1999-1-1' and '2022-1-1'
 or  (( '1999-1-1' between r.StartDate and r.EndDate)and('2022-1-1' between r.StartDate and r.EndDate))
)
Group by R.id,R.Description,R.PriceD


alter procedure ShowFoodStates @d1 date ,@d2 date
as 
begin
select F.Id,F.FoodName ,F.Price,sum(Fo.quantity)as 'Quantity sold',sum(Fo.quantity*F.Price) as 'Total Income'
from Rooms R join Reservations Rs on 
R.Id = Rs.RoomId
join OrderFoods Fo 
on Fo.RoomId = R.Id
join Foods F on F.id = Fo.FoodId 
where R.Id in (
select RoomId from Reservations r
where r.StartDate between @d1 and @d2
 or  r.EndDate between @d1 and @d2
 or  (( @d1 between r.StartDate and r.EndDate)and(@d2 between r.StartDate and r.EndDate))
)
group by F.Id,F.FoodName,F.Price
end

create procedure ShowFoodStatesS1 
@d1 date ,@d2 date
as 
begin
select F.Id,F.FoodName ,F.Price ,sum(Fo.quantity) as 'Quantity sold',sum(Fo.quantity*F.Price) as 'Total Income'
from OrderFoods Fo
join Foods F
on F.Id = Fo.FoodId
where Fo.OrderDate between @d1 and @d2
group by F.Id,F.FoodName,F.Price
end


select F.Id,F.FoodName ,F.Price ,sum(Fo.quantity) as 'Quantity sold',sum(Fo.quantity*F.Price) as 'Total Income'
from OrderFoods Fo
join Foods F
on F.Id = Fo.FoodId
where Fo.OrderDate between '1999-2-2' and '2022-1-1'


group by F.Id,F.FoodName,F.Price



create proc ShowServiceStates @d1 date ,@d2 date
as 
select S.Id,S.TypeService, count(*) as 'order times',sum(CAST(S.Price AS int)) as 'Total Income'
from OrderServices So
join Services S
on S.Id = So.ServiceId
where So.SOrderDate between @d1 and @d2
group by S.Id,S.TypeService 
end

select S.Id,S.TypeService ,count(*) as 'order times',sum(CAST(S.Price AS int)) as 'Total Income'
from OrderServices So
join Services S
on S.Id = So.ServiceId
where So.SOrderDate between '1999-2-2' and '2022-1-1'
group by S.Id,S.TypeService 

 
exec ShowFoodStates '1999-2-2','2022-1-1'



create proc clientBillRoom @idclient int
as
select R.Id ,R.Description,R.PriceD,Rs.StartDate as 'From',Rs.EndDate as 'To',DATEDIFF(DAY,Rs.StartDate,Rs.EndDate) as 'Days of Reservations' ,
     R.PriceD as 'Price par day',R.PriceD*DATEDIFF(DAY,Rs.StartDate,Rs.EndDate) as 'Total price'

from Reservations Rs
join Rooms R on R.id = Rs.RoomId
where Rs.UserId = 3

create proc clientBillFood @idclient int
as
select R.id as 'Room Id'  ,F.FoodName,F.Price,fo.quantity,F.Price*Fo.quantity as 'Total Price'

from Reservations Rs
join Rooms R on R.id = Rs.RoomId
join OrderFoods fo on fo.RoomId = Rs.RoomId and fo.OrderDate between Rs.StartDate and Rs.EndDate
join Foods F on fo.FoodId=F.Id
where Rs.UserId = 3


create proc clientBillservice @idclient int
as

select Rs.RoomId ,S.TypeService,S.Price,so.SOrderDate as 'Order Date'

from Reservations Rs
join Rooms R on R.id = Rs.RoomId
join OrderServices so on so.RoomId = Rs.RoomId and so.SOrderDate between Rs.StartDate and Rs.EndDate
join Services S on so.ServiceId=S.Id
where Rs.UserId = 3











join OrderFoods fo on fo.RoomId=R.id 
and fo.OrderDate between Rs.StartDate and Rs.EndDate
join OrderServices so on so.RoomId=R.id 
and so.OrderDate between Rs.StartDate and Rs.EndDate
join Foods 