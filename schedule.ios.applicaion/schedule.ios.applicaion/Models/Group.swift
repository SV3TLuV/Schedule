//
//  Group.swift
//  schedule.ios.application
//
//  Created by Иван Светлов on 11.06.2023.
//

import Foundation

struct Group: Identifiable, Codable, Hashable, Equatable {
    var id: Int
    var name: String
    var number: String
    var enrollmentYear: Int
    var term: Term
    var speciality: Speciality
    var mergedGroups: [Group]
    var isDeleted: Bool
}
