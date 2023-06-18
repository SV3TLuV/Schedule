//
//  Teacher.swift
//  schedule.ios.application
//
//  Created by Иван Светлов on 11.06.2023.
//

import Foundation

struct Teacher: Identifiable, Codable, Hashable, Equatable {
    var id: Int
    var name: String
    var surname: String
    var middleName: String
    var email: String
    var isDeleted: Bool
    
    func getFio() -> String {
        return "\(String(middleName.first!)).\(String(name.first!)). \(surname)"
    }
}
