using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.MockedModel
{
    public class MockedProduct
    {
        public static Product[] Objects
        {
            get
            {
                Product[] models = new Product[]
                    {
                        new Product(){ 
                            Id = 1, 
                            Name = "AB", 
                            ProductType = new ProductType(){
                                Id = 1,
                                Name = "Type1"
                            } 
                        },
                        new Product(){
                            Id = 3,
                            Name = "CD",
                            ProductType = new ProductType(){
                                Id = 1,
                                Name = "Type1"
                            }
                        },
                        new Product(){
                            Id = 2,
                            Name = "BC",
                            ProductType = new ProductType(){
                                Id = 2,
                                Name = "Type2"
                            }
                        },
                        new Product(){
                            Id = 4,
                            Name = "BC",
                            ProductType = new ProductType(){
                                Id = 2,
                                Name = "Type2"
                            }
                        },
                        new Product(){
                            Id = 5,
                            Name = "BDS",
                            ProductType = new ProductType(){
                                Id = 2,
                                Name = "Type2"
                            }
                        },
                        new Product(){
                            Id = 6,
                            Name = "ADD",
                            ProductType = new ProductType(){
                                Id = 1,
                                Name = "Type1"
                            }
                        },
                        new Product(){
                            Id = 7,
                            Name = "SDE",
                            ProductType = new ProductType(){
                                Id = 1,
                                Name = "Type1"
                            }
                        },
                        new Product(){
                            Id = 8,
                            Name = "BDSW",
                            ProductType = new ProductType(){
                                Id = 1,
                                Name = "Type1"
                            }
                        },
                        new Product(){
                            Id = 9,
                            Name = "AAADE",
                            ProductType = new ProductType(){
                                Id = 1,
                                Name = "Type1"
                            }
                        },
                        new Product(){
                            Id = 10,
                            Name = "NFFD",
                            ProductType = new ProductType(){
                                Id = 1,
                                Name = "Type1"
                            }
                        },
                        new Product(){
                            Id = 11,
                            Name = "FFES",
                            ProductType = new ProductType(){
                                Id = 2,
                                Name = "Type2"
                            }
                        },
                        new Product(){
                            Id = 12,
                            Name = "GGDWE",
                            ProductType = new ProductType(){
                                Id = 2,
                                Name = "Type2"
                            }
                        }
                    };

                return models;
            }
        }
    }
}
