using System.ComponentModel.DataAnnotations;
using FluentAssertions;
using Xunit.Categories;

namespace WaveApps.GraphQLBuilder.Tests
{
    public class WaveQueryBuilderTests
    {
        [Fact]
        [UnitTest]
        public void ListBusinesses()
        {
            var query = new WaveQueryBuilder()
                .WithBusinesses(
                    new BusinessConnectionQueryBuilder()
                        .WithEdges(new BusinessEdgeQueryBuilder()
                            .WithNode(new BusinessQueryBuilder()
                                .WithId()
                                .WithName())))
                .Build();

            query.Should().BeEquivalentTo("query{businesses{edges{node{id,name}}}}");
        }

        [Fact]
        [UnitTest]
        public void GetUser()
        {
            var query = new WaveQueryBuilder()
                .WithUser(new UserQueryBuilder()
                    .WithId()
                    .WithFirstName()
                    .WithLastName()
                    .WithDefaultEmail()
                    .WithCreatedAt()
                    .WithCreatedAt()
                    .WithModifiedAt()
                ).Build();

            query.Should().BeEquivalentTo("query{user{id,firstName,lastName,defaultEmail,createdAt,modifiedAt}}");
        }

        [Fact]
        [UnitTest]
        public void ListInvoicesByCustomer()
        {
            var businessIdParameter = new GraphQlQueryParameter<Guid?>("businessId", Guid.Parse("b534b077-5640-4fa1-b8e3-f50da66c8215"));
            var sortParameter = new GraphQlQueryParameter<IEnumerable<InvoiceSort>>("sort", $"[{GraphQlTypes.InvoiceSort}!]!", new[] { InvoiceSort.StatusAsc });
            var page = new GraphQlQueryParameter<int?>("page", 1);
            var pageSize = new GraphQlQueryParameter<int?>("pageSize", 5);

            var query = new WaveQueryBuilder()
                .WithBusiness(new BusinessQueryBuilder()
                    .WithId()
                    .WithInvoices(new InvoiceConnectionQueryBuilder()
                        .WithPageInfo(new OffsetPageInfoQueryBuilder()
                            .WithAllFields())
                        .WithEdges(new InvoiceEdgeQueryBuilder()
                            .WithNode(new InvoiceQueryBuilder()
                                .WithId()
                                .WithInvoiceNumber()
                                .WithDueDate()
                                .WithAmountDue(new MoneyQueryBuilder()
                                    .WithAllFields())
                                .WithTotal(new MoneyQueryBuilder()
                                    .WithAllFields())
                                .WithStatus()
                                .WithCustomer(new CustomerQueryBuilder()
                                    .WithEmail())
                            )), 
                        sortParameter, page: page, pageSize: pageSize), 
                    id: businessIdParameter)
                .WithParameter(businessIdParameter)
                .WithParameter(page)
                .WithParameter(pageSize)
                .WithParameter(sortParameter)
                .Build();

            query.Should().BeEquivalentTo("query($businessId:ID=\"b534b077-5640-4fa1-b8e3-f50da66c8215\",$page:Int=1,$pageSize:Int=5,$sort:[InvoiceSort!]!){business(id:$businessId){id,invoices(page:$page,pageSize:$pageSize,sort:$sort){pageInfo{currentPage,totalPages,totalCount},edges{node{id,invoiceNumber,dueDate,amountDue{minorUnitValue,value,currency{code,symbol,name,plural,exponent}},total{minorUnitValue,value,currency{code,symbol,name,plural,exponent}},status,customer{email}}}}}}");
        }
    }
}